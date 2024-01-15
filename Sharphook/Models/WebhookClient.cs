using System.Text;
using System.Net;
using System.Net.Http.Headers;

using Sharphook.Models.ResponseObjects;
using Sharphook.Exceptions;
using Sharphook.Models.Partials;
using Sharphook.DataTypes;

using Newtonsoft.Json;

namespace Sharphook.Models
{
	public class WebhookClient
	{
        readonly static HttpClient HttpClient = new HttpClient();
		readonly static SemaphoreSlim requestLock = new SemaphoreSlim(1, 1);
        public readonly byte ApiVersion = 10;

		public WebhookClient() {}

		public void Dispose()
		{
			HttpClient.Dispose();
            requestLock.Dispose();
		}

		private void throwExceptionFromStatusCode(HttpResponseMessage httpResponseMessage)
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

        internal float parseRatelimitHeader(HttpResponseMessage responseMessage, bool useClock = false)
        {
            HttpHeaders headers = responseMessage.Headers;
            IEnumerable<string> resetAfter;

            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            bool resetAfterHeadersExist = headers.TryGetValues("X-Ratelimit-Reset-After", out resetAfter);
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

		internal async Task<HttpResponseMessage> Request(HttpMethod httpMethod, Uri uri, HttpContent? content = null, string acceptContentType = "application/json")
		{
            await requestLock.WaitAsync();

            HttpRequestMessage message = new HttpRequestMessage
			{
				RequestUri = uri,
				Method = httpMethod,
				Content = content
			};
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptContentType));

            HttpResponseMessage responseMessage = await HttpClient.SendAsync(message);

            string remaining = responseMessage.Headers.GetValues("X-Ratelimit-Remaining").First() ?? "0";

            if (responseMessage.StatusCode != HttpStatusCode.TooManyRequests && remaining == "0")
            {
                int waitTime = Convert.ToInt32(parseRatelimitHeader(responseMessage) * 1_000);

                await Task.Delay(waitTime);

                requestLock.Release();
                return await Request(httpMethod, uri, content, acceptContentType);
            }
            else if (responseMessage.StatusCode == HttpStatusCode.TooManyRequests)
            {
                int retryAfterInSeconds = Convert.ToInt32(responseMessage.Headers.GetValues("Retry-After").First());
                int retryAfterInMilliseconds = retryAfterInSeconds * 1_000;

                await Task.Delay(retryAfterInMilliseconds + 1_000);

                requestLock.Release();
                return await Request(httpMethod, uri, content, acceptContentType);
            }

            if (!responseMessage.IsSuccessStatusCode) { throwExceptionFromStatusCode(responseMessage); }

            message.Dispose();
            requestLock.Release();

			return responseMessage;
        }
		
		internal async Task<ReturnObject> Get<ReturnObject>(string uri)
		{
			HttpResponseMessage responseMessage = await Request(HttpMethod.Get, new Uri(uri));

            string jsonString = await responseMessage.Content.ReadAsStringAsync();
            ReturnObject jsonData = JsonConvert.DeserializeObject<ReturnObject>(jsonString)!;

            responseMessage.Dispose();
            return jsonData;
        }

		internal async Task<ReturnObject> Post<ReturnObject>(string uri, object requestBody)
		{
			string serializedRequestBody = JsonConvert.SerializeObject(requestBody);
            StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await Request(HttpMethod.Post, new Uri(uri), httpContent);

            string jsonString = await responseMessage.Content.ReadAsStringAsync();
            ReturnObject jsonData = JsonConvert.DeserializeObject<ReturnObject>(jsonString)!;

            responseMessage.Dispose();
            return jsonData;
        }

		internal async Task<ReturnObject> Patch<ReturnObject>(string uri, object requestBody)
		{
            string serializedRequestBody = JsonConvert.SerializeObject(requestBody);
            StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await Request(HttpMethod.Patch, new Uri(uri), httpContent);

            string jsonString = await responseMessage.Content.ReadAsStringAsync();
            ReturnObject jsonData = JsonConvert.DeserializeObject<ReturnObject>(jsonString)!;

            responseMessage.Dispose();
            return jsonData;
        }

		internal async Task Delete(string uri)
		{
            HttpResponseMessage responseMessage = await Request(HttpMethod.Delete, new Uri(uri));
            responseMessage.Dispose();
        }

        // Multipart

        internal async Task<ReturnObject> PostMultipart<ReturnObject>(string uri, object requestBody, List<SharphookFile> files)
        {
            string serializedRequestBody = JsonConvert.SerializeObject(requestBody);

            using (StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json"))
            using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
			{
                formDataContent.Add(httpContent, "payload_json");

                for (int i = 0; i < files.Count; i++)
				{
					SharphookFile file= files[i];
                    StreamContent streamContent = new StreamContent(file.Stream);

                    streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    formDataContent.Add(streamContent, $"files[{i}]", file.FileName);
                }

                HttpResponseMessage responseMessage = await Request(HttpMethod.Post, new Uri(uri), formDataContent);

                if (!responseMessage.IsSuccessStatusCode) { throwExceptionFromStatusCode(responseMessage); }

                string stringJsonData = await responseMessage.Content.ReadAsStringAsync();
                ReturnObject jsonData = JsonConvert.DeserializeObject<ReturnObject>(stringJsonData)!;

                return jsonData;
            }
        }

        internal async Task<ReturnObject> PatchMultipart<ReturnObject>(string uri, object requestBody, List<SharphookFile> files)
        {
            string serializedRequestBody = JsonConvert.SerializeObject(requestBody);

            using (StringContent httpContent = new StringContent(serializedRequestBody, Encoding.UTF8, "application/json"))
			using (MultipartFormDataContent formDataContent = new MultipartFormDataContent())
			{
                formDataContent.Add(httpContent, "payload_json");

				for (int i = 0; i < files.Count; i++) 
				{
                    SharphookFile file = files[i];
                    StreamContent streamContent = new StreamContent(file.Stream);

                    streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    formDataContent.Add(streamContent, $"files[{i}]", file.FileName);
                }

                HttpResponseMessage responseMessage = await Request(HttpMethod.Patch, new Uri(uri), formDataContent);

                if (!responseMessage.IsSuccessStatusCode) { throwExceptionFromStatusCode(responseMessage); }

                string stringJsonData = await responseMessage.Content.ReadAsStringAsync();
                ReturnObject jsonData = JsonConvert.DeserializeObject<ReturnObject>(stringJsonData)!;

                return jsonData;
            }
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
}