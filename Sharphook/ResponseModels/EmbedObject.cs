#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseObjects;

internal record EmbedObject
{
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("description")]
	public string? Description { get; set; }

	[JsonPropertyName("url")]
	public string? Url { get; set; }

	[JsonPropertyName("color")]
	public uint? Color { get; set; }

	[JsonPropertyName("timestamp")]
	public string? Timestamp { get; set; }

	[JsonPropertyName("footer")]
	public EmbedFooterObject? Footer { get; set; }

	[JsonPropertyName("image")]
	public EmbedImageObject? Image { get; set; }

	[JsonPropertyName("thumbnail")]
	public EmbedThumbnailObject? Thumbnail { get; set; }

	[JsonPropertyName("author")]
	public EmbedAuthorObject? Author { get; set; }

	[JsonPropertyName("fields")]
	public List<EmbedFieldObject>? Fields { get; set; }

	public EmbedObject() { }

	internal EmbedObject(Embed embed)
	{
		Title = embed.Title;
		Description = embed.Description;
		Url = embed.Url;
		Fields = new List<EmbedFieldObject>();

		if (embed.Color != null)
			Color = embed.Color.Value;

		if (embed.Timestamp != null)
			Timestamp = embed.Timestamp?.ToString("o");

		if (embed.Footer != null)
			Footer = new EmbedFooterObject(embed.Footer);

		if (embed.Image != null)
			Image = new EmbedImageObject(embed.Image);

		if (embed.Thumbnail != null)
			Thumbnail = new EmbedThumbnailObject(embed.Thumbnail);

		if (embed.Author != null)
			Author = new EmbedAuthorObject(embed.Author);

		if (embed.Fields != null)
			foreach (EmbedField field in embed.Fields)
				Fields.Add(new EmbedFieldObject(field));
	}
}
