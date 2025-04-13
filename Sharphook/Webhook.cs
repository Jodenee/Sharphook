using Sharphook.Partials;
using Sharphook.ResponseObjects;

namespace Sharphook;

public sealed class Webhook : PartialWebhook
{
	public int Type { get; private set; }
	public string Name { get; private set; }
	public ulong GuildId { get; private set; }
	public ulong ChannelId { get; private set; }
	public ulong? ApplicationId { get; private set; }
	public Asset? Avatar { get; private set; }
	public User? Creator { get; private set; }

	internal Webhook(WebhookClient client, WebhookObject webhookObject)
		: base(client, Convert.ToUInt64(webhookObject.Id), webhookObject.Token)
	{
		Type = webhookObject.Type;
		Name = webhookObject.Name;
		GuildId = Convert.ToUInt64(webhookObject.GuildId);
		ChannelId = Convert.ToUInt64(webhookObject.ChannelId);
		ApplicationId = webhookObject.ApplicationId != null 
			? Convert.ToUInt64(webhookObject.ApplicationId) 
			: null;
		Avatar = webhookObject.AvatarHash != null
			? Asset.FromAvatar(client, Id, webhookObject.AvatarHash)
			: null;

		if (webhookObject.Creator != null)
			Creator = new User(client, webhookObject.Creator);
	}
}
