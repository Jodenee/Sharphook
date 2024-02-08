using System.Dynamic;

using Sharphook.DataTypes;
using Sharphook.Enums;
using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models.Partials
{
	public class PartialWebhook
	{
		WebhookClient Client;
		public ulong Id { get; private set; }
		public string Token { get; private set; }
		public string BaseUrl 
		{
			get => $"https://discord.com/api/v{Client.ApiVersion}/webhooks/{Id}/{Token}";
		}

		public PartialWebhook(WebhookClient client, ulong webhookId, string webhookToken)
		{
			Client = client;
			Id = webhookId;
			Token = webhookToken;
		}

		public async Task<Message?> SendMessageAsync(string? content = null, List<Embed>? embeds = null, bool waitForMessage = false, OptionalSendMessageInfo? optionalSendMessageInfo = null)
		{
			string requestUrl = $"{BaseUrl}?wait={waitForMessage}";

			OptionalSendMessageInfo optionalInfo = optionalSendMessageInfo ?? new OptionalSendMessageInfo();
			List<EmbedObject> embedObjects = new List<EmbedObject>();

			if (embeds != null)
			{
				foreach (Embed embed in embeds)
				{
					embedObjects.Add(embed._ToEmbedObject());
				}
			}

			object requestBody = new
			{
				content,
				embeds = embedObjects,
				username = optionalInfo.UsernameOverride,
				avatar_url = optionalInfo.AvatarUrlOverride,
				tts = optionalInfo.TTS,
				flags = optionalInfo.MessageFlags,
				thread_name = optionalInfo.ThreadName
			};


			MessageObject messageObject;


			if (optionalInfo.Files != null)
			{
                messageObject = await Client.PostMultipart<MessageObject>(requestUrl, requestBody, optionalInfo.Files);
            }
            else
			{
                messageObject = await Client.Post<MessageObject>(requestUrl, requestBody);
            }

			if (waitForMessage)
			{
				return new Message(Client, messageObject);
			}

			return null;
		}

		public async Task<ThreadMessage?> SendMessageInThreadAsync(ulong threadId, string? content = null, List<Embed>? embeds = null, bool waitForMessage = false, OptionalSendMessageInfo? optionalSendMessageInfo = null)
		{
			string requestUrl = $"{BaseUrl}?wait={waitForMessage}&thread_id={threadId}";

			OptionalSendMessageInfo optionalInfo = optionalSendMessageInfo ?? new OptionalSendMessageInfo();
			List<EmbedObject> embedObjects = new List<EmbedObject>();

			if (embeds != null)
			{
				foreach (Embed embed in embeds)
				{
					embedObjects.Add(embed._ToEmbedObject());
				}
			}

			object requestBody = new
			{
				content,
				embeds = embedObjects,
				username = optionalInfo.UsernameOverride,
				avatar_url = optionalInfo.AvatarUrlOverride,
				tts = optionalInfo.TTS,
				flags = optionalInfo.MessageFlags,
				thread_name = optionalInfo.ThreadName
			};

            ThreadMessageObject threadMessageObject;

            if (optionalInfo.Files != null)
            {
                threadMessageObject = await Client.PostMultipart<ThreadMessageObject>(requestUrl, requestBody, optionalInfo.Files);
            }
            else
            {
                threadMessageObject = await Client.Post<ThreadMessageObject>(requestUrl, requestBody);
            }

			if (waitForMessage)
			{
                return new ThreadMessage(Client, threadMessageObject);
            }

            return null;
        }

		public async Task<EditedMessage> EditMessageAsync(ulong messageId, string? content = null, List<Embed>? embeds = null, OptionalEditMessageInfo? optionalEditInfo = null)
		{
			string requestUrl = $"{BaseUrl}/messages/{messageId}";
			OptionalEditMessageInfo optionalInfo = optionalEditInfo ?? new OptionalEditMessageInfo();

            List<EmbedObject> embedObjects = new List<EmbedObject>();
            List<object> attachments = new List<object>();
            dynamic requestBody = new ExpandoObject();

			if (embeds != null)
			{
				foreach (Embed embed in embeds)
				{
					embedObjects.Add(embed._ToEmbedObject());
				}
			}

            if (optionalInfo.Files != null)
			{
				for (int i = 0; i < optionalInfo.Files.Count; i++)
				{
					SharphookFile file = optionalInfo.Files[i];
					attachments.Add(file._ToObject(i));
				}
			}


            if (content != null) { requestBody.content = content; }
			if (embeds != null) { requestBody.embeds = embedObjects; }
			if (attachments != null) { requestBody.attachments = attachments; }


            EditedMessageObject editedMessageObject;

			if (optionalInfo.Files != null)
			{
                editedMessageObject = await Client.PatchMultipart<EditedMessageObject>(requestUrl, requestBody, optionalInfo.Files);
            }
			else
			{
                editedMessageObject = await Client.Patch<EditedMessageObject>(requestUrl, requestBody);
            }

			return new EditedMessage(Client, editedMessageObject);
        }

		public async Task<EditedThreadMessage> EditMessageInThreadAsync(ulong threadId, ulong messageId, string? content = null, List<Embed>? embeds = null, OptionalEditMessageInfo? optionalEditInfo = null)
		{
			string requestUrl = $"{BaseUrl}/messages/{messageId}?thread_id={threadId}";
            OptionalEditMessageInfo optionalInfo = optionalEditInfo ?? new OptionalEditMessageInfo();

            List<EmbedObject> embedObjects = new List<EmbedObject>();
            List<object> attachments = new List<object>();
            dynamic requestBody = new ExpandoObject();

            if (embeds != null)
			{
				foreach (Embed embed in embeds)
				{
					embedObjects.Add(embed._ToEmbedObject());
				}
			}

            if (optionalInfo.Files != null)
            {
                for (int i = 0; i < optionalInfo.Files.Count; i++)
                {
                    SharphookFile file = optionalInfo.Files[i];
                    attachments.Add(file._ToObject(i));
                }
            }


            if (content != null) { requestBody.content = content; }
            if (embeds != null) { requestBody.embeds = embedObjects; }
            if (attachments != null) { requestBody.attachments = attachments; }


            EditedThreadMessageObject editedThreadMessageObject;

            if (optionalInfo.Files != null)
            {
                editedThreadMessageObject = await Client.PatchMultipart<EditedThreadMessageObject>(requestUrl, requestBody, optionalInfo.Files);
            }
            else
            {
                editedThreadMessageObject = await Client.Patch<EditedThreadMessageObject>(requestUrl, requestBody);
            }

			return new EditedThreadMessage(Client, editedThreadMessageObject);
        }

		public async Task DeleteMessageAsync(ulong messageId)
		{
			string requestUrl = $"{BaseUrl}/messages/{messageId}";

			await Client.Delete(requestUrl);
		}

		public async Task DeleteMessageInThreadAsync(ulong threadid, ulong messageId)
		{
			string requestUrl = $"{BaseUrl}/messages/{messageId}?thread_id={threadid}";

			await Client.Delete(requestUrl);
		}

        public async Task<Message> GetMessageAsync(ulong messageId)
        {
            string requestUrl = $"{BaseUrl}/messages/{messageId}";

            MessageObject messageObject = await Client.Get<MessageObject>(requestUrl);

            return new Message(Client, messageObject);
        }

        public async Task<ThreadMessage> GetMessageInThreadAsync(ulong messageId, ulong threadId)
        {
            string requestUrl = $"{BaseUrl}/messages/{messageId}?thread_id={threadId}";

            ThreadMessageObject threadMessageObject = await Client.Get<ThreadMessageObject>(requestUrl);

            return new ThreadMessage(Client, threadMessageObject);
        }

        public async Task<Webhook> EditNameAsync(string newName)
		{
			object requestBody = new
			{
				name = newName
			};

			WebhookObject webhookObject = await Client.Patch<WebhookObject>(BaseUrl, requestBody);
            Webhook webhook = new Webhook(Client, webhookObject);

            return webhook;
        }
		
        public async Task<Webhook> EditAvatarAsync(string avatarFilePath, FileContentType avatarFileContentType)
        {
            byte[] imageByteArray = await File.ReadAllBytesAsync(avatarFilePath);
            string base64ImageData = Convert.ToBase64String(imageByteArray);

            string contentType = "";

            switch (avatarFileContentType)
            {
                case FileContentType.PNG:
                    contentType = "image/png";
                    break;
                case FileContentType.JPEG:
                    contentType = "image/jpeg";
                    break;
                case FileContentType.GIF:
                    contentType = "image/gif";
                    break;
            }

            string imageData = $"data:{contentType};base64,{base64ImageData}";

            object requestBody = new
            {
                avatar = imageData
            };

            WebhookObject webhookObject = await Client.Patch<WebhookObject>(BaseUrl, requestBody);
			Webhook webhook = new Webhook(Client, webhookObject);

            return webhook;
        }
	}
}