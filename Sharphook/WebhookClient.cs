using Sharphook.Partials;
using Sharphook.ResponseObjects;
using Sharphook.Utility.Helpers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Sharphook;

public sealed class WebhookClient
{
	private readonly HttpClient _httpClient;

	public byte ApiVersion { get; private set; }
	public string BaseUrl { get; private set; }
	public string BaseCDNUrl { get; private set; }
	public AllowedMentions AllowedMentions { get; set; }

	public WebhookClient(AllowedMentions? allowedMentions = null, TimeSpan? timeout = null)
	{
		ApiVersion = 10;
		BaseUrl = $"https://discord.com/api/v{ApiVersion}";
		BaseCDNUrl = "https://cdn.discordapp.com";

		_httpClient = new HttpClient
		{
			Timeout = timeout ?? TimeSpan.FromSeconds(10)
		};
		AllowedMentions ??= AllowedMentions.All;
	}

	public void Dispose()
	{
		_httpClient.Dispose();
	}

	internal async Task<HttpResponseMessage> Request(
		HttpMethod method,
		Uri uri,
		ICollection<NameValueHeaderValue>? headers = null,
		HttpContent? content = null,
		string? accept = null,
		SemaphoreSlim? requestLock = null)
	{
		if (requestLock != null)
			await requestLock.WaitAsync();

		HttpRequestMessage request = new HttpRequestMessage
		{
			Method = method,
			RequestUri = uri,
			Content = content,
		};

		if (headers is not null)
			foreach (NameValueHeaderValue header in headers)
				request.Headers.Add(header.Name, header.Value);

		request.Headers.Add("Accept", accept ?? "application/json");

		HttpResponseMessage response = await _httpClient.SendAsync(request);
		RatelimitInformation? ratelimitInformation = HttpHelper.ParseRatelimitInformation(response.Headers);
		RatelimitedInformation? ratelimitedInformation = HttpHelper.ParseRatelimitedInformation(response.Headers);

		if (response.StatusCode != HttpStatusCode.TooManyRequests && ratelimitInformation != null)
		{
			if (ratelimitInformation.Remaining == 0)
			{
				await Task.Delay(ratelimitInformation.SafeResetAfterInMilliseconds);

				request.Dispose();
				response.Dispose();
				requestLock?.Release();

				return await Request(method, uri, headers, content, accept, requestLock);
			}
		}
		else if (response.StatusCode == HttpStatusCode.TooManyRequests && ratelimitedInformation != null)
		{
			await Task.Delay(ratelimitedInformation.SafeRetryAfterInMilliseconds);

			request.Dispose();
			response.Dispose();
			requestLock?.Release();

			return await Request(method, uri, headers, content, accept, requestLock);
		}

		if (!response.IsSuccessStatusCode)
			HttpHelper.ThrowFromStatusCode(response);

		request.Dispose();
		requestLock?.Release();

		return response;
	}

	internal async Task<ReturnObject?> Get<ReturnObject>(
		Uri uri,
		List<NameValueHeaderValue>? headers = null,
		SemaphoreSlim? requestLock = null)
	{
		HttpResponseMessage response = await Request(HttpMethod.Get, uri, headers, requestLock: requestLock);

		if (response.StatusCode == HttpStatusCode.NoContent)
			return default;

		string responseBody = await response.Content.ReadAsStringAsync();
		ReturnObject? jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

		response.Dispose();
		return jsonResponseBody!;
	}

	internal async Task<ReturnObject?> Post<ReturnObject>(
		Uri uri,
		object requestBody,
		List<NameValueHeaderValue>? headers = null,
		SemaphoreSlim? requestLock = null)
	{
		string serializedRequestBody = JsonSerializer.Serialize(requestBody);
		StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json");
		HttpResponseMessage response = await Request(HttpMethod.Post, uri, headers, httpContent, requestLock: requestLock);

		if (response.StatusCode == HttpStatusCode.NoContent)
			return default;

		string responseBody = await response.Content.ReadAsStringAsync();
		ReturnObject jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

		response.Dispose();
		return jsonResponseBody;
	}

	internal async Task<ReturnObject?> Patch<ReturnObject>(
		Uri uri,
		object requestBody,
		List<NameValueHeaderValue>? headers = null,
		SemaphoreSlim? requestLock = null)
	{
		string serializedRequestBody = JsonSerializer.Serialize(requestBody);
		StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json");
		HttpResponseMessage response = await Request(HttpMethod.Patch, uri, headers, httpContent, requestLock: requestLock);

		if (response.StatusCode == HttpStatusCode.NoContent)
			return default;

		string responseBody = await response.Content.ReadAsStringAsync();
		ReturnObject jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

		response.Dispose();
		return jsonResponseBody;
	}

	internal async Task Delete(Uri uri, List<NameValueHeaderValue>? headers = null, SemaphoreSlim? requestLock = null)
	{
		HttpResponseMessage response = await Request(HttpMethod.Delete, uri, headers, requestLock: requestLock);
		response.Dispose();
	}

	internal async Task<byte[]> GetFromCDN(Uri uri, SemaphoreSlim? requestLock = null)
	{
		if (requestLock != null)
			await requestLock.WaitAsync();

		HttpResponseMessage response = await Request(
			HttpMethod.Get,
			uri,
			accept: "image/png, image/jpeg, image/webp, image/gif",
			requestLock: requestLock);

		byte[] bytes = await response.Content.ReadAsByteArrayAsync();

		response.Dispose();

		return bytes;
	}

	// Multipart

	internal async Task<ReturnObject?> PostMultipart<ReturnObject>(
		Uri uri,
		object requestBody,
		List<SharphookFile> files,
		List<NameValueHeaderValue>? headers = null,
		SemaphoreSlim? requestLock = null)
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

		using StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json");
		using MultipartFormDataContent formDataContent = new MultipartFormDataContent
		{
			{ httpContent, "payload_json" }
		};

		for (int i = 0; i < files.Count; i++)
		{
			SharphookFile file = files[i];
			StreamContent streamContent = new StreamContent(streams[i]);

			streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
			formDataContent.Add(streamContent, $"files[{i}]", file.FileName);
		}

		HttpResponseMessage response = await Request(
			HttpMethod.Post,
			uri,
			headers,
			formDataContent,
			requestLock: requestLock);

		foreach (FileStream stream in streams)
			await stream.DisposeAsync();

		if (response.StatusCode == HttpStatusCode.NoContent)
			return default;

		string responseBody = await response.Content.ReadAsStringAsync();
		ReturnObject jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

		response.Dispose();
		return jsonResponseBody;
	}

	internal async Task<ReturnObject?> PatchMultipart<ReturnObject>(
		Uri uri,
		object requestBody,
		List<SharphookFile> files,
		List<NameValueHeaderValue>? headers = null,
		SemaphoreSlim? requestLock = null)
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

		using StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json");
		using MultipartFormDataContent formDataContent = new MultipartFormDataContent
		{
			{ httpContent, "payload_json" }
		};

		for (int i = 0; i < files.Count; i++)
		{
			SharphookFile file = files[i];
			StreamContent streamContent = new StreamContent(streams[i]);

			streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
			formDataContent.Add(streamContent, $"files[{i}]", file.FileName);
		}

		HttpResponseMessage response = await Request(HttpMethod.Patch, uri, headers, formDataContent, requestLock: requestLock);

		foreach (FileStream stream in streams)
			await stream.DisposeAsync();

		if (response.StatusCode == HttpStatusCode.NoContent)
			return default;

		string responseBody = await response.Content.ReadAsStringAsync();
		ReturnObject jsonResponseBody = JsonSerializer.Deserialize<ReturnObject>(responseBody)!;

		response.Dispose();
		return jsonResponseBody;
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
			throw new ArgumentException($"webhook url provided ({webhookUrl}) could not be parsed. " +
				$"Please check the url for any mistakes and try again.");

		return new PartialWebhook(this, webhookId, webhookToken);
	}

	public PartialWebhook GetPartialWebhook(ulong webhookId, string webhookToken)
	{
		return new PartialWebhook(this, webhookId, webhookToken);
	}

	public async Task<Webhook> GetWebhook(string webhookUrl)
	{
		WebhookObject? webhookObject = await Get<WebhookObject>(new Uri(webhookUrl));

		return new Webhook(this, webhookObject!);
	}

	public async Task<Webhook> GetWebhook(ulong webhookId, string webhookToken)
	{
		WebhookObject? webhookObject = await Get<WebhookObject>(
			new Uri($"{BaseUrl}/webhooks/{webhookId}/{webhookToken}"));

		return new Webhook(this, webhookObject!);
	}
}