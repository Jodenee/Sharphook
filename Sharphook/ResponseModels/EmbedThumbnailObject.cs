#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseObjects;

internal record EmbedThumbnailObject
{
	[JsonPropertyName("url")]
	public string Url { get; set; }

	[JsonPropertyName("proxy_url")]
	public string? ProxiedUrl { get; set; }

	[JsonPropertyName("width")]
	public int? Width { get; set; }

	[JsonPropertyName("height")]
	public int? Hieght { get; set; }

	public EmbedThumbnailObject() { }

	internal EmbedThumbnailObject(EmbedThumbnail embedThumbnail)
	{
		Url = embedThumbnail.Url;
	}
}
