using Sharphook.ResponseObjects;

namespace Sharphook;

public sealed class Embed
{
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? Url { get; set; }
	public DateTime? Timestamp { get; set; }
	public SharphookColor? Color { get; set; }
	public EmbedFooter? Footer { get; set; }
	public EmbedImage? Image { get; set; }
	public EmbedThumbnail? Thumbnail { get; set; }
	public EmbedAuthor? Author { get; set; }
	public List<EmbedField> Fields { get; set; }
	public int TotalCharacters
	{
		get
		{
			int totalCharacters = 0;

			if (Title != null)
				totalCharacters += Title.Length;
			if (Description != null)
				totalCharacters += Description.Length;
			if (Footer != null)
				totalCharacters += Footer.Text.Length;
			if (Author != null)
				totalCharacters += Author.Name.Length;

			foreach (EmbedField embedField in Fields)
			{
				totalCharacters += embedField.Name.Length + embedField.Value.Length;
			}

			return totalCharacters;
		}
	}


	public Embed(
		string? embedTitle = null,
		string? embedDescription = null,
		string? embedUrl = null,
		DateTime? timestamp = null,
		SharphookColor? color = null,
		EmbedFooter? footer = null,
		EmbedImage? image = null,
		EmbedThumbnail? thumbnail = null,
		EmbedAuthor? author = null,
		List<EmbedField>? fields = null)
	{
		Title = embedTitle;
		Description = embedDescription;
		Url = embedUrl;
		Timestamp = timestamp;
		Color = color;
		Footer = footer;
		Image = image;
		Thumbnail = thumbnail;
		Author = author;
		Fields = fields != null ? fields : new List<EmbedField>();
	}

	internal Embed(EmbedObject embedObject)
	{
		Title = embedObject.Title;
		Description = embedObject.Description;
		Url = embedObject.Url;
		Fields = new List<EmbedField>();

		if (embedObject.Color != null)
			Color = new SharphookColor((uint)embedObject.Color);

		if (embedObject.Timestamp != null)
			Timestamp = DateTime.Parse(embedObject.Timestamp);

		if (embedObject.Footer != null)
			Footer = new EmbedFooter(embedObject.Footer);

		if (embedObject.Image != null)
			Image = new EmbedImage(embedObject.Image);

		if (embedObject.Thumbnail != null)
			Thumbnail = new EmbedThumbnail(embedObject.Thumbnail);

		if (embedObject.Author != null)
			Author = new EmbedAuthor(embedObject.Author);

		if (embedObject.Fields != null)
			foreach (EmbedFieldObject fieldObject in embedObject.Fields)
				Fields.Add(new EmbedField(fieldObject));
	}
}
