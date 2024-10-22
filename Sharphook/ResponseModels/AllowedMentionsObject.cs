#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseObjects;

internal record AllowedMentionsObject
{
	[JsonPropertyName("parse")]
	public List<string> Parse { get; set; }

	[JsonPropertyName("roles")]
	public ulong[] Roles { get; set; }

	[JsonPropertyName("users")]
	public ulong[] Users { get; set; }
}
