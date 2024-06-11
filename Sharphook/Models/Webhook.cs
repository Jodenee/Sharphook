using Sharphook.Models.Partials;
using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models;

public class Webhook : PartialWebhook
{
	WebhookClient Client { get; set; }
	public short Type { get; private set; }
	public string Name { get; private set; }
	public string GuildId { get; private set; }
	public string ChannelId { get; private set; }
	public string? ApplicationId { get; private set; }
	public string? Avatar { get; private set; }
	public User? Creator { get; private set; }

	public Webhook(WebhookClient client, WebhookObject webhookObject)
		: base(client, Convert.ToUInt64(webhookObject.Id), webhookObject.Token)
	{
		Client = client;
		Type = webhookObject.Type;
		Name = webhookObject.Name;
		GuildId = webhookObject.GuildId;
		ChannelId = webhookObject.ChannelId;
		ApplicationId = webhookObject.ApplicationId;
		Avatar = webhookObject.Avatar;

		if (webhookObject.Creator != null)
			Creator = new User(client, webhookObject.Creator);
	}
}
