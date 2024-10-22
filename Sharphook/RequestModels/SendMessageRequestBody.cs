using Sharphook.ResponseObjects;
using System.Text.Json.Serialization;

namespace Sharphook.RequestModels;

internal sealed record SendMessageRequestBody
{
	[JsonPropertyName("content")]
	public string? Content { get; set; }

	[JsonPropertyName("username")]
	public string? Username { get; set; }

	[JsonPropertyName("avatar_url")]
	public string? AvatarUrl { get; set; }

	[JsonPropertyName("tts")]
	public bool? TTS { get; set; }

	[JsonPropertyName("embeds")]
	public List<EmbedObject>? Embeds { get; set; }

	[JsonPropertyName("allowed_mentions")]
	public AllowedMentionsObject? AllowedMentions { get; set; }

	[JsonPropertyName("flags")]
	public int? Flags { get; set; }

	[JsonPropertyName("thread_name")]
	public string? ThreadName { get; set; }

	[JsonPropertyName("applied_tags")]
	public List<ulong>? AppliedTags { get; set; }
}
