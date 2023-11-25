using System.Text;
using Newtonsoft.Json;
using Sharphook.Models.ResponseObjects;
using Sharphook.Exceptions;
using System.Net;
using Sharphook.Models.Partials;

namespace Sharphook.Models
{
	public class WebhookClient
	{
		private static HttpClient HttpClient = new HttpClient();

		public WebhookClient() {}

		public void close()
		{
			HttpClient.Dispose();
		}

		private void throwExceptionFromStatusCode(HttpResponseMessage httpResponseMessage)
		{
			throw httpResponseMessage.StatusCode switch
			{
				HttpStatusCode.BadRequest => new BadRequest(httpResponseMessage),
				HttpStatusCode.Unauthorized => new Unauthorized(httpResponseMessage),
				HttpStatusCode.Forbidden => new Forbidden(httpResponseMessage),
				HttpStatusCode.NotFound => new NotFound(httpResponseMessage),
				HttpStatusCode.TooManyRequests => new TooManyRequests(httpResponseMessage),
				HttpStatusCode.BadGateway => new BadGateway(httpResponseMessage),
				_ => new SharphookHttpException(httpResponseMessage),
			};
		}
		
		internal async Task<ApiResponce<ReturnObject>> Get<ReturnObject>(string url)
		{
			Uri requestUri = new Uri(url);
			HttpResponseMessage responceMessage = await HttpClient.GetAsync(requestUri);

			if (!responceMessage.IsSuccessStatusCode) { throwExceptionFromStatusCode(responceMessage); }

			string stringJsonData = await responceMessage.Content.ReadAsStringAsync();
			ReturnObject jsonData = JsonConvert.DeserializeObject<ReturnObject>(stringJsonData)!;

			return new ApiResponce<ReturnObject>(jsonData, responceMessage);
		}

		internal async Task<ApiResponce<ReturnObject>> Post<ReturnObject>(string requestUrl, object requestBody)
		{
			Uri requestUri = new Uri(requestUrl);
			string serializedJsonObject = JsonConvert.SerializeObject(requestBody);

            StringContent httpContent = new StringContent(serializedJsonObject, Encoding.UTF8, "application/json");
			HttpResponseMessage responceMessage = await HttpClient.PostAsync(requestUri, httpContent);

            if (!responceMessage.IsSuccessStatusCode) { throwExceptionFromStatusCode(responceMessage); }

			string stringJsonData = await responceMessage.Content.ReadAsStringAsync();
			ReturnObject jsonData = JsonConvert.DeserializeObject<ReturnObject>(stringJsonData)!;

			return new ApiResponce<ReturnObject>(jsonData, responceMessage);
		}

		internal async Task<ApiResponce<ReturnObject>> Patch<ReturnObject>(string requestUrl, object requestBody)
		{
			Uri requestUri = new Uri(requestUrl);
            string serializedJsonObject = JsonConvert.SerializeObject(requestBody);

            StringContent httpContent = new StringContent(serializedJsonObject, Encoding.UTF8, "application/json");
			HttpResponseMessage responceMessage = await HttpClient.PatchAsync(requestUri, httpContent);

			if (!responceMessage.IsSuccessStatusCode) { throwExceptionFromStatusCode(responceMessage); }

			string stringJsonData = await responceMessage.Content.ReadAsStringAsync();
			ReturnObject jsonData = JsonConvert.DeserializeObject<ReturnObject>(stringJsonData)!;

			return new ApiResponce<ReturnObject>(jsonData, responceMessage);
		}

		internal async Task<HttpResponseMessage> Delete(string requestUrl)
		{
			Uri requestUri = new Uri(requestUrl);
			HttpResponseMessage responceMessage = await HttpClient.DeleteAsync(requestUri);

			if (!responceMessage.IsSuccessStatusCode) { throwExceptionFromStatusCode(responceMessage); }

			return responceMessage;
		}

		public PartialWebhook GetPartialWebhook(ulong webhookId, string webhookToken)
		{
			return new PartialWebhook(this, webhookId, webhookToken);
		}

		public async Task<Webhook> GetWebhook(string webhookUrl)
		{
			ApiResponce<WebhookObject> apiResponce = await Get<WebhookObject>(webhookUrl);
			Webhook webhook = new Webhook(this, apiResponce.ResponceObject!);

			return webhook;
		}

		public async Task<Webhook> GetWebhook(ulong webhookId, string webhookToken)
		{
			ApiResponce<WebhookObject> apiResponce = await Get<WebhookObject>($"https://discord.com/api/webhooks/{webhookId}/{webhookToken}");
			Webhook webhook = new Webhook(this, apiResponce.ResponceObject!);

			return webhook;
		}
	}
}