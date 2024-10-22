#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseObjects;

internal record EmbedAuthorObject
{
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("url")]
	public string? Url { get; set; }

	[JsonPropertyName("icon_url")]
	public string? IconUrl { get; set; }

	[JsonPropertyName("proxy_icon_url")]
	public string? ProxiedIconUrl { get; set; }

	public EmbedAuthorObject() { }

	internal EmbedAuthorObject(EmbedAuthor embedAuthor)
	{
		Name = embedAuthor.Name;
		Url = embedAuthor.Url;
		IconUrl = embedAuthor.IconUrl;
	}
}
