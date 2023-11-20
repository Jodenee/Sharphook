using System;
using System.Threading.Tasks;
using Sharphook.Models.Partials;
using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models
{
	public class Webhook : PartialWebhook
	{
		private WebhookClient Client { get; }
		public Int16 Type { get; private set; }
		public string Name { get; private set; }
		public string GuildId { get; private set; }
		public string ChannelId { get; private set; }
		public string? ApplicationId { get; private set; }
		public string? Avatar { get; private set; }
		public PartialUser? User { get; private set; }

		public Webhook(WebhookClient client, WebhookObject webhookResponce):base(client, Convert.ToUInt64(webhookResponce.id), webhookResponce.token) 
		{
			Client = client;
			Type = webhookResponce.type;
			Name = webhookResponce.name;
			GuildId = webhookResponce.guild_id;
			ChannelId = webhookResponce.channel_id;
			ApplicationId = webhookResponce.application_id;
			Avatar = webhookResponce.avatar;

			if (webhookResponce.user != null) { User = new PartialUser(client, webhookResponce.user); }
		}
	}
}
