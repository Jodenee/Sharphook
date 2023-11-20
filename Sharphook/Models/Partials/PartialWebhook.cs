using Sharphook.Enums;
using Sharphook.Models.ResponseObjects;
using System.Net.WebSockets;

namespace Sharphook.Models.Partials
{
	public class PartialWebhook
	{
		private WebhookClient Client { get; }
		public ulong Id { get; private set; }
		public string Token { get; private set; }
		public string BaseUrl { get; private set; }

		public PartialWebhook(WebhookClient client, ulong webhookId, string webhookToken)
		{
			Client = client;
			Id = webhookId;
			Token = webhookToken;
			BaseUrl = $"https://discord.com/api/webhooks/{Id}/{Token}";
		}

		private static EmbedObject EmbedToEmbedObject(Embed embed)
		{
			EmbedObject embedObject = new EmbedObject();

			embedObject.title = embed.Title;
			embedObject.description = embed.Description;
			embedObject.url = embed.Url;
			embedObject.fields = new List<EmbedObjectField>();

			if (embed.Color != null)
			{
				embedObject.color = embed.Color.Value;
			}

			if (embed.Timestamp != null)
			{
				embedObject.timestamp = embed.Timestamp?.ToString("o");
			}

			if (embed.Footer != null)
			{
				EmbedObjectFooter embedObjectFooter = new EmbedObjectFooter();

				embedObjectFooter.text = embed.Footer.Text;
				embedObjectFooter.icon_url = embed.Footer.IconUrl;

				embedObject.footer = embedObjectFooter;
			}

			if (embed.Image != null)
			{
				EmbedObjectImage embedObjectImage = new EmbedObjectImage();

				embedObjectImage.url = embed.Image.Url;

				embedObject.image = embedObjectImage;
			}

			if (embed.Thumbnail != null)
			{
				EmbedObjectThumbnail embedObjectThumbnail = new EmbedObjectThumbnail();

				embedObjectThumbnail.url = embed.Thumbnail.Url;

				embedObject.thumbnail = embedObjectThumbnail;
			}

			if (embed.Author != null)
			{
				EmbedObjectAuthor embedObjectAuthor = new EmbedObjectAuthor();

				embedObjectAuthor.name = embed.Author.Name;
				embedObjectAuthor.url = embed.Author.Url;
				embedObjectAuthor.icon_url = embed.Author.IconUrl;

				embedObject.author = embedObjectAuthor;
			}

			if (embed.Fields != null)
			{
				foreach (EmbedField field in embed.Fields)
				{
					EmbedObjectField embedObjectField = new EmbedObjectField();

					embedObjectField.name = field.Name;
					embedObjectField.value = field.Value;
					embedObjectField.inline = field.InLine;

					embedObject.fields.Add(embedObjectField);
				}
			}

			return embedObject;
		}

		public async Task<AbstractedResponce<Message>> SendMessage(string? content = null, List<Embed>? embeds = null, bool waitForMessage = false, OptionalSendMessageInfo? optionalSendMessageInfo = null)
		{
			string requestUrl = $"{BaseUrl}?wait={waitForMessage}";

			OptionalSendMessageInfo optionalInfo = optionalSendMessageInfo ?? new OptionalSendMessageInfo();
			List<EmbedObject> embedObjects = new List<EmbedObject>();

			if (embeds != null)
			{
				for (byte i = 0; i < embeds.Count; i++)
				{
					Embed embed = embeds[i];

					embedObjects.Add(EmbedToEmbedObject(embed));
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


			ApiResponce<MessageObject> apiResponce = await Client.Post<MessageObject>(requestUrl, requestBody);
			RatelimitInfo ratelimitInfo = new RatelimitInfo(apiResponce.HttpResponse.Headers);

			if (waitForMessage)
			{
				Message message = new Message(Client, apiResponce.ResponceObject!);

				return new AbstractedResponce<Message>(message, ratelimitInfo);
			}

			return new AbstractedResponce<Message>(null, ratelimitInfo);
		}

		public async Task<AbstractedResponce<ThreadMessage>> SendMessageInThread(ulong threadId, string? content = null, List<Embed>? embeds = null, bool waitForMessage = false, OptionalSendMessageInfo? optionalSendMessageInfo = null)
		{
			string requestUrl = $"{BaseUrl}?wait={waitForMessage}&thread_id={threadId}";

			OptionalSendMessageInfo optionalInfo = optionalSendMessageInfo ?? new OptionalSendMessageInfo();
			List<EmbedObject> embedObjects = new List<EmbedObject>();

			if (embeds != null)
			{
				foreach (Embed embed in embeds)
				{
					embedObjects.Add(EmbedToEmbedObject(embed));
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


			ApiResponce<ThreadMessageObject> apiResponce = await Client.Post<ThreadMessageObject>(requestUrl, requestBody);
			RatelimitInfo ratelimitInfo = new RatelimitInfo(apiResponce.HttpResponse.Headers);


			if (waitForMessage)
			{
				ThreadMessage threadMessage = new ThreadMessage(Client, apiResponce.ResponceObject!);

				return new AbstractedResponce<ThreadMessage>(threadMessage, ratelimitInfo);
			}

			return new AbstractedResponce<ThreadMessage>(null, ratelimitInfo);
		}

		public async Task<AbstractedResponce<EditedMessage>> EditMessage(ulong messageId, string? content = null, List<Embed>? embeds = null)
		{
			string requestUrl = $"{BaseUrl}/messages/{messageId}";

			List<EmbedObject> embedObjects = new List<EmbedObject>();

			if (embeds != null)
			{
				foreach (Embed embed in embeds)
				{
					embedObjects.Add(EmbedToEmbedObject(embed));
				}
			}

			object requestBody = new
			{
				content,
				embeds = embedObjects
			};

			ApiResponce<EditedMessageObject> apiResponce = await Client.Patch<EditedMessageObject>(requestUrl, requestBody);
			RatelimitInfo ratelimitInfo = new RatelimitInfo(apiResponce.HttpResponse.Headers);
			EditedMessage message = new EditedMessage(Client, apiResponce.ResponceObject!);

			return new AbstractedResponce<EditedMessage>(message, ratelimitInfo);
		}

		public async Task<AbstractedResponce<EditedThreadMessage>> EditMessageInThread(ulong threadId, ulong messageId, string? content = null, List<Embed>? embeds = null)
		{
			string requestUrl = $"{BaseUrl}/messages/{messageId}?thread_id={threadId}";

			List<EmbedObject> embedObjects = new List<EmbedObject>();

			if (embeds != null)
			{
				foreach (Embed embed in embeds)
				{
					embedObjects.Add(EmbedToEmbedObject(embed));
				}
			}

			object requestBody = new
			{
				content,
				embeds = embedObjects
			};

			ApiResponce<EditedThreadMessageObject> apiResponce = await Client.Patch<EditedThreadMessageObject>(requestUrl, requestBody);
			RatelimitInfo ratelimitInfo = new RatelimitInfo(apiResponce.HttpResponse.Headers);
			EditedThreadMessage message = new EditedThreadMessage(Client, apiResponce.ResponceObject!);

			return new AbstractedResponce<EditedThreadMessage>(message, ratelimitInfo);
		}

		public async Task DeleteMessage(ulong messageId)
		{
			string requestUrl = $"{BaseUrl}/messages/{messageId}";

			await Client.Delete(requestUrl);
		}

		public async Task DeleteMessageInThread(ulong threadid, ulong messageId)
		{
			string requestUrl = $"{BaseUrl}/messages/{messageId}?thread_id={threadid}";

			await Client.Delete(requestUrl);
		}

		public async Task<Webhook> Edit(string? newName = null, string? avatarFilePath = null, FileContentType? avatarFileContentType = null)
		{
			string? imageData = null;

			if (avatarFilePath != null)
			{
				ArgumentNullException.ThrowIfNull(avatarFileContentType, "avatarFileContentType");

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

				imageData = $"data:{contentType};base64,{base64ImageData}";

				Console.WriteLine(imageData);
			}

			object requestBody = new
			{
				name = newName,
				avatar = imageData
			};

			ApiResponce<WebhookObject> apiResponce = await Client.Patch<WebhookObject>(BaseUrl, requestBody);
			
			return new Webhook(Client, apiResponce.ResponceObject!);
		}

		public async Task Delete()
		{
			await Client.Delete(BaseUrl);
		}
	}
}