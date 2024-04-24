using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Sharphook.Models.ResponseObjects;

public class WebhookObject
{
	[JsonPropertyName("type")]
	public short Type { get; set; }

	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("application_id")]
	public string? ApplicationId { get; set; }

	[JsonPropertyName("token")]
	public string Token { get; set; }

	[JsonPropertyName("url")]
	public string Url { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("guild_id")]
	public string GuildId { get; set; }

	[JsonPropertyName("channel_id")]
	public string ChannelId { get; set; }

	[JsonPropertyName("avatar")]
	public string? Avatar { get; set; }

	[JsonPropertyName("user")]
	public UserObject? Creator { get; set; }
}