#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Sharphook.Utility.Enums;
using System.Text.Json.Serialization;

namespace Sharphook.ResponseObjects;

internal sealed record MessageObject
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("channel_id")]
	public string ChannelId { get; set; }

	[JsonPropertyName("author")]
	public UserObject Author { get; set; }

	[JsonPropertyName("content")]
	public string Content { get; set; }

	[JsonPropertyName("timestamp")]
	public string CreatedAt { get; set; }

	[JsonPropertyName("type")]
	public byte Type { get; set; }

	[JsonPropertyName("embeds")]
	public List<EmbedObject> Embeds { get; set; }

	[JsonPropertyName("pinned")]
	public bool Pinned { get; set; }

	[JsonPropertyName("mention_everyone")]
	public bool MentionsEveryone { get; set; }

	[JsonPropertyName("mentions")]
	public List<UserObject> MentionedUsers { get; set; }

	[JsonPropertyName("mention_roles")]
	public List<string> MentionedRoles { get; set; }

	[JsonPropertyName("tts")]
	public bool IsTTS { get; set; }

	[JsonPropertyName("webhook_id")]
	public string WebhookId { get; set; }

	[JsonPropertyName("attachments")]
	public List<AttachmentObject> Attachments { get; set; }

	[JsonPropertyName("flags")]
	public MessageFlag Flags { get; set; }

	[JsonPropertyName("edited_timestamp")]
	public DateTime? EditedAt { get; set; }

	[JsonPropertyName("position")]
	public int? Position { get; set; }
}
