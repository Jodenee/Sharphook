#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.Models.ResponseObjects;

public class UserObject
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("username")]
	public string Username { get; set; }

	[JsonPropertyName("global_name")]
	public string? GlobalName { get; set; }

	[JsonPropertyName("avatar")]
	public string? Avatar { get; set; }

	[JsonPropertyName("public_flags")]
	public int PublicFlags { get; set; }

	[JsonPropertyName("bot")]
	public bool? IsBot { get; set; }
}