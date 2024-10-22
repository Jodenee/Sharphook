using Sharphook.Partials;
using Sharphook.ResponseObjects;

namespace Sharphook;

public sealed class Webhook : PartialWebhook
{
	public short Type { get; private set; }
	public string Name { get; private set; }
	public string GuildId { get; private set; }
	public string ChannelId { get; private set; }
	public string? ApplicationId { get; private set; }
	public Asset? Avatar { get; private set; }
	public User? Creator { get; private set; }

	internal Webhook(WebhookClient client, WebhookObject webhookObject)
		: base(client, Convert.ToUInt64(webhookObject.Id), webhookObject.Token)
	{
		Type = webhookObject.Type;
		Name = webhookObject.Name;
		GuildId = webhookObject.GuildId;
		ChannelId = webhookObject.ChannelId;
		ApplicationId = webhookObject.ApplicationId;
		Avatar = webhookObject.AvatarHash != null
			? Asset.FromAvatar(_client, Id, webhookObject.AvatarHash)
			: null;

		if (webhookObject.Creator != null)
			Creator = new User(client, webhookObject.Creator);
	}
}
