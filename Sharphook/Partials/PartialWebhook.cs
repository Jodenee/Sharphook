using Sharphook.RequestModels;
using Sharphook.ResponseObjects;
using Sharphook.Utility.Helpers;
using System.Dynamic;
using System.Net.Http.Headers;

namespace Sharphook.Partials;

public class PartialWebhook
{
	private protected readonly WebhookClient _client;
	private protected readonly SemaphoreSlim _requestLock = new SemaphoreSlim(1, 1);

	public ulong Id { get; private set; }
	public string Token { get; private set; }
	public string BaseUrl
	{
		get => $"{_client.BaseUrl}/webhooks/{Id}/{Token}";
	}

	public PartialWebhook(WebhookClient client, ulong webhookId, string webhookToken)
	{
		_client = client;
		Id = webhookId;
		Token = webhookToken;
	}

	public async Task<Message?> SendMessageAsync(
		string? content = null,
		ICollection<Embed>? embeds = null,
		bool waitForMessage = true,
		OptionalSendMessageInfo? optionalInfo = null)
	{
		Uri requestUri = new Uri($"{BaseUrl}?wait={waitForMessage}");

		OptionalSendMessageInfo optionalSendInfo = optionalInfo ?? new OptionalSendMessageInfo();
		List<EmbedObject> embedObjects = new List<EmbedObject>();

		if (embeds != null)
			foreach (Embed embed in embeds)
				embedObjects.Add(new EmbedObject(embed));

		SendMessageRequestBody requestBody = new SendMessageRequestBody
		{
			Content = content,
			Username = optionalSendInfo.UsernameOverride,
			AvatarUrl = optionalSendInfo.AvatarUrlOverride,
			TTS = optionalSendInfo.TTS,
			Embeds = embedObjects,
			AllowedMentions = (optionalSendInfo.AllowedMentions ?? _client.AllowedMentions)
				.ToAllowedMentionsObject(),
			Flags = (int?)optionalSendInfo.MessageFlags,
			ThreadName = optionalSendInfo.ThreadName,
			AppliedTags = optionalSendInfo.ApplyTags
		};

		MessageObject? messageObject;

		if (optionalSendInfo.Files != null)
			messageObject = await _client.PostMultipart<MessageObject>(
				requestUri,
				requestBody,
				optionalSendInfo.Files,
				requestLock: _requestLock);
		else
			messageObject = await _client.Post<MessageObject>(requestUri, requestBody, requestLock: _requestLock);


		if (messageObject != null)
			return new Message(_client, messageObject!);
		else
			return null;
	}

	public async Task<Message?> SendMessageInThreadAsync(
		ulong threadId,
		string? content = null,
		ICollection<Embed>? embeds = null,
		bool waitForMessage = true,
		OptionalSendMessageInfo? optionalInfo = null)
	{
		Uri requestUri = new Uri($"{BaseUrl}?wait={waitForMessage}&thread_id={threadId}");

		OptionalSendMessageInfo optionalSendInfo = optionalInfo ?? new OptionalSendMessageInfo();
		List<EmbedObject> embedObjects = new List<EmbedObject>();

		if (embeds != null)
			foreach (Embed embed in embeds)
				embedObjects.Add(new EmbedObject(embed));

		SendMessageInThreadRequestBody requestBody = new SendMessageInThreadRequestBody
		{
			Content = content,
			Username = optionalSendInfo.UsernameOverride,
			AvatarUrl = optionalSendInfo.AvatarUrlOverride,
			TTS = optionalSendInfo.TTS,
			Embeds = embedObjects,
			AllowedMentions = (optionalSendInfo.AllowedMentions ?? _client.AllowedMentions)
				.ToAllowedMentionsObject(),
			Flags = (int?)optionalSendInfo.MessageFlags
		};

		MessageObject? messageObject;

		if (optionalSendInfo.Files != null)
			messageObject = await _client.PostMultipart<MessageObject>(
				requestUri,
				requestBody,
				optionalSendInfo.Files,
				requestLock: _requestLock);
		else
			messageObject = await _client.Post<MessageObject>(requestUri, requestBody, requestLock: _requestLock);


		if (messageObject != null)
			return new Message(_client, messageObject!);
		else
			return null;
	}

	public async Task<Message> EditMessageAsync(
		ulong messageId,
		string? content = null,
		ICollection<Embed>? embeds = null,
		OptionalEditMessageInfo? optionalInfo = null)
	{
		Uri requestUri = new Uri($"{BaseUrl}/messages/{messageId}");

		OptionalEditMessageInfo optionalEditInfo = optionalInfo ?? new OptionalEditMessageInfo();
		dynamic requestBody = new ExpandoObject();
		List<EmbedObject> embedObjects = new List<EmbedObject>();
		List<object> attachments = new List<object>();

		if (embeds is not null)
			foreach (Embed embed in embeds)
				embedObjects.Add(new EmbedObject(embed));

		if (content is not null)
			requestBody.content = content;
		if (embeds is not null)
			requestBody.embeds = embedObjects;
		if (optionalEditInfo.AllowedMentions != null)
			requestBody.allowed_mentions = (optionalEditInfo.AllowedMentions ?? _client.AllowedMentions)
				.ToAllowedMentionsObject();

		MessageObject messageObject;

		if (optionalEditInfo.Files != null)
		{
			for (int i = 0; i < optionalEditInfo.Files.Count; i++)
			{
				SharphookFile file = optionalEditInfo.Files[i];
				attachments.Add(file.ToObject(i));
			}

			requestBody.attachments = attachments;
			messageObject = await _client.PatchMultipart<MessageObject>(
				requestUri,
				requestBody,
				optionalEditInfo.Files,
				requestLock: _requestLock);
		}
		else
			messageObject = await _client.Patch<MessageObject>(requestUri, requestBody, requestLock: _requestLock);

		return new Message(_client, messageObject);
	}

	public async Task<Message> EditMessageInThreadAsync(
		ulong threadId,
		ulong messageId,
		string? content = null,
		ICollection<Embed>? embeds = null,
		OptionalEditMessageInfo? optionalInfo = null)
	{
		Uri requestUri = new Uri($"{BaseUrl}/messages/{messageId}?thread_id={threadId}");

		OptionalEditMessageInfo optionalEditInfo = optionalInfo ?? new OptionalEditMessageInfo();
		List<EmbedObject> embedObjects = new List<EmbedObject>();
		List<object> attachments = new List<object>();
		dynamic requestBody = new ExpandoObject();

		if (embeds is not null)
		{
			foreach (Embed embed in embeds)
			{
				embedObjects.Add(new EmbedObject(embed));
			}
		}

		if (content is not null)
			requestBody.content = content;
		if (embeds is not null)
			requestBody.embeds = embedObjects;
		if (optionalEditInfo.AllowedMentions != null)
			requestBody.allowed_mentions = (optionalEditInfo.AllowedMentions ?? _client.AllowedMentions)
				.ToAllowedMentionsObject();

		MessageObject editedThreadMessageObject;

		if (optionalEditInfo.Files != null)
		{
			for (int i = 0; i < optionalEditInfo.Files.Count; i++)
			{
				SharphookFile file = optionalEditInfo.Files[i];
				attachments.Add(file.ToObject(i));
			}

			requestBody.attachments = attachments;
			editedThreadMessageObject = await _client.PatchMultipart<MessageObject>(requestUri, requestBody, optionalEditInfo.Files);
		}
		else
			editedThreadMessageObject = await _client.Patch<MessageObject>(requestUri, requestBody, requestLock: _requestLock);

		return new Message(_client, editedThreadMessageObject);
	}

	public async Task DeleteMessageAsync(ulong messageId)
	{
		Uri requestUri = new Uri($"{BaseUrl}/messages/{messageId}");

		await _client.Delete(requestUri, requestLock: _requestLock);
	}

	public async Task DeleteMessageInThreadAsync(ulong threadid, ulong messageId)
	{
		Uri requestUri = new Uri($"{BaseUrl}/messages/{messageId}?thread_id={threadid}");

		await _client.Delete(requestUri, requestLock: _requestLock);
	}

	public async Task<Message> GetMessageAsync(ulong messageId)
	{
		Uri requestUri = new Uri($"{BaseUrl}/messages/{messageId}");

		MessageObject? messageObject = await _client.Get<MessageObject>(requestUri, requestLock: _requestLock);

		return new Message(_client, messageObject!);
	}

	public async Task<Message> GetMessageInThreadAsync(ulong messageId, ulong threadId)
	{
		Uri requestUri = new Uri($"{BaseUrl}/messages/{messageId}?thread_id={threadId}");

		MessageObject? messageObject = await _client.Get<MessageObject>(requestUri, requestLock: _requestLock);

		return new Message(_client, messageObject!);
	}

	public async Task<Webhook> EditNameAsync(string newName, string? reason = null)
	{
		List<NameValueHeaderValue> headers = new List<NameValueHeaderValue>();
		object requestBody = new
		{
			name = newName
		};

		if (reason != null)
			headers.Add(new NameValueHeaderValue("X-Audit-Log-Reason", reason));

		WebhookObject? webhookObject = await _client.Patch<WebhookObject>(new Uri(BaseUrl), requestBody, headers, _requestLock);

		return new Webhook(_client, webhookObject!);
	}

	public async Task<Webhook> EditAvatarAsync(string avatarFilePath, string? reason = null)
	{
		string fileExtension = Path.GetExtension(avatarFilePath);
		List<NameValueHeaderValue> headers = new List<NameValueHeaderValue>();
		object requestBody = new
		{
			avatar = await ImageFormatHelper.ToDataURIAsync(avatarFilePath, ImageFormatHelper.Parse(fileExtension))
		};

		if (reason != null)
			headers.Add(new NameValueHeaderValue("X-Audit-Log-Reason", reason));

		WebhookObject? webhookObject = await _client.Patch<WebhookObject>(new Uri(BaseUrl), requestBody, headers, _requestLock);

		return new Webhook(_client, webhookObject!);
	}

	public void Dispose()
	{
		_requestLock.Dispose();
	}
}
