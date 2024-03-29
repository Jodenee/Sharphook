using System.Text;
using System.Text.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Sharphook.Exceptions;
using Sharphook.Models.ResponseObjects;
using Sharphook.Models.Partials;

namespace Sharphook.Models;

public class WebhookClient
{
	readonly static HttpClient HttpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
	readonly static SemaphoreSlim requestLock = new SemaphoreSlim(1, 1);
	public readonly byte ApiVersion = 10;

	public WebhookClient() { }

	public void Dispose()
	{
		HttpClient.Dispose();
		requestLock.Dispose();
	}

	private void ThrowExceptionFromStatusCode(HttpResponseMessage httpResponseMessage)
	{
		throw httpResponseMessage.StatusCode switch
		{
			HttpStatusCode.BadRequest => new BadRequest(httpResponseMessage),
			HttpStatusCode.Unauthorized => new Unauthorized(httpResponseMessage),
			HttpStatusCode.Forbidden => new Forbidden(httpResponseMessage),
			HttpStatusCode.NotFound => new NotFound(httpResponseMessage),
			HttpStatusCode.BadGateway => new BadGateway(httpResponseMessage),
			_ => new SharphookHttpException(httpResponseMessage)
		};
	}

	internal float ParseRatelimitHeader(HttpResponseMessage responseMessage, bool useClock = false)
	{
		HttpResponseHeaders headers = responseMessage.Headers;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
		bool resetAfterHeadersExist = headers.TryGetValues("X-Ratelimit-Reset-After", out IEnumerable<string> resetAfter);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

		if (useClock || !resetAfterHeadersExist)
		{
			long reset = Convert.ToInt64(headers.GetValues("X-Ratelimit-Reset").First());
			DateTime utcNow = DateTime.UtcNow;
			DateTimeOffset resetOffset = DateTimeOffset.FromUnixTimeSeconds(reset);

			return Convert.ToSingle((resetOffset - utcNow).TotalSeconds);
		}


		return Convert.ToSingle(resetAfter!.First());
	}

	internal async Task<HttpResponseMessage> Request(
		HttpMethod httpMethod,
		Uri uri,
		List<NameValueHeaderValue>? headers,
		HttpContent? content = null,
		string acceptContentType = "application/json"
	)
	{
		await requestLock.WaitAsync();

		HttpRequestMessage message = new HttpRequestMessage
		{
			RequestUri = uri,
			Method = httpMethod,
			Content = content
		};
		message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptContentType));

		if (headers is not null)
		{
			foreach (NameValueHeaderValue header in headers)
            {
				message.Headers.Add(header.Name, header.Value);
			}
		}

		HttpResponseMessage responseMessage = await HttpClient.SendAsync(message);

		string remaining = responseMessage.Headers.GetValues("X-Ratelimit-Remaining").First() ?? "0";

		if (responseMessage.StatusCode != HttpStatusCode.TooManyRequests && remaining == "0")
		{
			int waitTime = Convert.ToInt32(ParseRatelimitHeader(responseMessage) * 1_000);

			await Task.Delay(waitTime);

			message.Dispose();
			requestLock.Release();

			return await Request(httpMethod, uri, headers, content, acceptContentType);
		}
		else if (responseMessage.StatusCode == HttpStatusCode.TooManyRequests)
		{
			int retryAfterInSeconds = Convert.ToInt32(responseMessage.Headers.GetValues("Retry-After").First());
			int retryAfterInMilliseconds = retryAfterInSeconds * 1_000;

			await Task.Delay(retryAfterInMilliseconds + 1_000);

			message.Dispose();
			requestLock.Release();

			return await Request(httpMethod, uri, headers, content, acceptContentType);
		}

		if (!responseMessage.IsSuccessStatusCode) 
			ThrowExceptionFromStatusCode(responseMessage);

		message.Dispose();
		requestLock.Release();

		return responseMessage;
	}

	internal async Task<ReturnObject> Get<ReturnObject>(string uri, List<NameValueHeaderValue>? headers = null)
	{
		HttpResponseMessage responseMessage = await Request(HttpMethod.Get, new Uri(uri), headers);

		string responseBody = await responseMessage.Content.ReadAsStringAsync();
		ReturnObject? jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

		responseMessage.Dispose();
		return jsonResponseBody!;
	}

	internal async Task<ReturnObject> Post<ReturnObject>(string uri, object requestBody, List<NameValueHeaderValue>? headers = null)
	{
		string serializedRequestBody = JsonSerializer.Serialize(requestBody);
		StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json");

		HttpResponseMessage responseMessage = await Request(HttpMethod.Post, new Uri(uri), headers, httpContent);

		string responseBody = await responseMessage.Content.ReadAsStringAsync();
		ReturnObject jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

		responseMessage.Dispose();
		return jsonResponseBody;
	}

	internal async Task<ReturnObject> Patch<ReturnObject>(string uri, object requestBody, List<NameValueHeaderValue>? headers = null)
	{
		string serializedRequestBody = JsonSerializer.Serialize(requestBody);
		StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json");

		HttpResponseMessage responseMessage = await Request(HttpMethod.Patch, new Uri(uri), headers, httpContent);

		string responseBody = await responseMessage.Content.ReadAsStringAsync();
		ReturnObject jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

		responseMessage.Dispose();
		return jsonResponseBody;
	}

	internal async Task Delete(string uri, List<NameValueHeaderValue> headers)
	{
		HttpResponseMessage responseMessage = await Request(HttpMethod.Delete, new Uri(uri), headers);
		responseMessage.Dispose();
	}

	// Multipart

	internal async Task<ReturnObject> PostMultipart<ReturnObject>(string uri, object requestBody, List<SharphookFile> files, List<NameValueHeaderValue>? headers = null)
	{
		string serializedRequestBody = JsonSerializer.Serialize(requestBody);
		List<FileStream> streams = new List<FileStream>();
		long totalFileUploadSize = 0;

		foreach (SharphookFile file in files)
		{
			FileStream stream = file.Stream;

            streams.Add(stream);
            totalFileUploadSize += stream.Length;
		}

		if (totalFileUploadSize > 25_000_000) 
			throw new ArgumentException("Payload too large to upload. Make sure the total size of the files you are uploading is under 25MB.");

		using (StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json"))
		using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
		{
			formDataContent.Add(httpContent, "payload_json");

			for (int i = 0; i < files.Count; i++)
			{
				SharphookFile file = files[i];
				StreamContent streamContent = new StreamContent(streams[i]);

				streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
				formDataContent.Add(streamContent, $"files[{i}]", file.FileName);
			}

			HttpResponseMessage responseMessage = await Request(HttpMethod.Post, new Uri(uri), headers, formDataContent);

			foreach (FileStream stream in streams)
			{
				await stream.DisposeAsync();
			}

			string responseBody = await responseMessage.Content.ReadAsStringAsync();
			ReturnObject jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

            responseMessage.Dispose();
            return jsonResponseBody;
		}
	}

	internal async Task<ReturnObject> PatchMultipart<ReturnObject>(string uri, object requestBody, List<SharphookFile> files, List<NameValueHeaderValue>? headers = null)
	{
		string serializedRequestBody = JsonSerializer.Serialize(requestBody);
        List<FileStream> streams = new List<FileStream>();
        long totalFileUploadSize = 0;

        foreach (SharphookFile file in files)
        {
            FileStream stream = file.Stream;

            streams.Add(stream);
            totalFileUploadSize += stream.Length;
        }

        if (totalFileUploadSize > 25_000_000) 
			throw new ArgumentException("Payload too large to upload. Make sure the total size of the files you are uploading is under 25MB.");

		using (StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json"))
		using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
		{
			formDataContent.Add(httpContent, "payload_json");

			for (int i = 0; i < files.Count; i++)
			{
				SharphookFile file = files[i];
				StreamContent streamContent = new StreamContent(streams[i]);

				streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
				formDataContent.Add(streamContent, $"files[{i}]", file.FileName);
            }

			HttpResponseMessage responseMessage = await Request(HttpMethod.Patch, new Uri(uri), headers, formDataContent);

            foreach (FileStream stream in streams)
            {
                await stream.DisposeAsync();
            }

            string responseBody = await responseMessage.Content.ReadAsStringAsync();
			ReturnObject jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

            responseMessage.Dispose();
            return jsonResponseBody;
		}
	}

	public PartialWebhook GetPartialWebhook(string webhookUrl)
	{
		ulong webhookId;
		string webhookToken;

		Regex WebhookUrlRegex = new Regex(
			@"^.*(discord|discordapp)\.com\/api\/webhooks\/([\d]+)\/([a-z0-9_-]+)$",
			RegexOptions.Compiled |
			RegexOptions.IgnoreCase |
			RegexOptions.CultureInvariant
		);

		Match webhookUrlMatch = WebhookUrlRegex.Match(webhookUrl);

		Group webhookIdMatch = webhookUrlMatch.Groups[2];
		Group webhookTokenMatch = webhookUrlMatch.Groups[3];

		if (webhookIdMatch.Success && webhookTokenMatch.Success)
		{
			webhookId = Convert.ToUInt64(webhookIdMatch.Value);
			webhookToken = webhookTokenMatch.Value;
		}
		else
			throw new ArgumentException($"webhook url provided ({webhookUrl}) could not be parsed. Please check the url for any mistakes and try again.");

		return new PartialWebhook(this, webhookId, webhookToken);
	}

	public PartialWebhook GetPartialWebhook(ulong webhookId, string webhookToken)
	{
		return new PartialWebhook(this, webhookId, webhookToken);
	}

	public async Task<Webhook> GetWebhook(string webhookUrl)
	{
		WebhookObject webhookObject = await Get<WebhookObject>(webhookUrl);
		Webhook webhook = new Webhook(this, webhookObject);

		return webhook;
	}

	public async Task<Webhook> GetWebhook(ulong webhookId, string webhookToken)
	{
		WebhookObject webhookObject = await Get<WebhookObject>($"https://discord.com/api/webhooks/{webhookId}/{webhookToken}");
		Webhook webhook = new Webhook(this, webhookObject);

		return webhook;
	}
}