#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseObjects;

internal sealed record AttachmentObject
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("filename")]
	public string Filename { get; set; }

	[JsonPropertyName("description")]
	public string? Description { get; set; }

	[JsonPropertyName("content_type")]
	public string? ContentType { get; set; }

	[JsonPropertyName("size")]
	public int Size { get; set; }

	[JsonPropertyName("url")]
	public string Url { get; set; }

	[JsonPropertyName("proxy_url")]
	public string ProxyUrl { get; set; }

	[JsonPropertyName("height")]
	public int? Height { get; set; }

	[JsonPropertyName("width")]
	public int? Width { get; set; }
}