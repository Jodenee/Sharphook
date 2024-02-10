using Sharphook.Models.Partials;
using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models
{
	public class Webhook : PartialWebhook
	{
		WebhookClient Client { get; set; }
		public Int16 Type { get; private set; }
		public string Name { get; private set; }
		public string GuildId { get; private set; }
		public string ChannelId { get; private set; }
		public string? ApplicationId { get; private set; }
		public string? Avatar { get; private set; }
		public PartialUser? Creator { get; private set; }

		public Webhook(WebhookClient client, WebhookObject webhookResponce)
			: base(client, Convert.ToUInt64(webhookResponce.Id), webhookResponce.Token) 
		{
			Client = client;
			Type = webhookResponce.Type;
			Name = webhookResponce.Name;
			GuildId = webhookResponce.GuildId;
			ChannelId = webhookResponce.ChannelId;
			ApplicationId = webhookResponce.ApplicationId;
			Avatar = webhookResponce.Avatar;

			if (webhookResponce.Creator != null) { Creator = new PartialUser(client, webhookResponce.Creator); }
		}
	}
}
