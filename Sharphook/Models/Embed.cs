using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models;

public class Embed
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


	public Embed(string? embedTitle = null, string? embedDescription = null, string? embedUrl = null)
	{
		Title = embedTitle;
		Description = embedDescription;
		Url = embedUrl;
		Fields = new List<EmbedField>();
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
			Footer = new EmbedFooter(embedObject.Footer.Text, embedObject.Footer.IconUrl);

		if (embedObject.Image != null)
			Image = new EmbedImage(embedObject.Image.Url);

		if (embedObject.Thumbnail != null)
			Thumbnail = new EmbedThumbnail(embedObject.Thumbnail.Url);

		if (embedObject.Author != null)
			Author = new EmbedAuthor(embedObject.Author.Name, embedObject.Author.Url, embedObject.Author.IconUrl);

		if (embedObject.Fields != null)
		{
			foreach (EmbedObjectField fieldObject in embedObject.Fields)
			{
				Fields.Add(new EmbedField(fieldObject.Name, fieldObject.Value, fieldObject.Inline));
			}
		}
	}

	public void SetTimestampFromDatetime(DateTime? customTimestamp = null)
	{
		Timestamp = customTimestamp ?? DateTime.Now;
	}

	public int GetTotalCharacters()
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

	internal EmbedObject ToEmbedObject()
	{
		EmbedObject embedObject = new EmbedObject();

		embedObject.Title = Title;
		embedObject.Description = Description;
		embedObject.Url = Url;
		embedObject.Fields = new List<EmbedObjectField>();

		if (Color != null)
			embedObject.Color = Color.Value;

		if (Timestamp != null)
			embedObject.Timestamp = Timestamp?.ToString("o");

		if (Footer != null)
		{
			EmbedObjectFooter embedObjectFooter = new EmbedObjectFooter();

			embedObjectFooter.Text = Footer.Text;
			embedObjectFooter.IconUrl = Footer.IconUrl;

			embedObject.Footer = embedObjectFooter;
		}

		if (Image != null)
		{
			EmbedObjectImage embedObjectImage = new EmbedObjectImage();

			embedObjectImage.Url = Image.Url;

			embedObject.Image = embedObjectImage;
		}

		if (Thumbnail != null)
		{
			EmbedObjectThumbnail embedObjectThumbnail = new EmbedObjectThumbnail();

			embedObjectThumbnail.Url = Thumbnail.Url;

			embedObject.Thumbnail = embedObjectThumbnail;
		}

		if (Author != null)
		{
			EmbedObjectAuthor embedObjectAuthor = new EmbedObjectAuthor();

			embedObjectAuthor.Name = Author.Name;
			embedObjectAuthor.Url = Author.Url;
			embedObjectAuthor.IconUrl = Author.IconUrl;

			embedObject.Author = embedObjectAuthor;
		}

		if (Fields != null)
		{
			foreach (EmbedField field in Fields)
			{
				EmbedObjectField embedObjectField = new EmbedObjectField();

				embedObjectField.Name = field.Name;
				embedObjectField.Value = field.Value;
				embedObjectField.Inline = field.InLine;

				embedObject.Fields.Add(embedObjectField);
			}
		}

		return embedObject;
	}
}

public class EmbedFooter
{
	public string Text;
	public string? IconUrl;

	public EmbedFooter(string footerText, string? footerIconUrl = null)
	{
		Text = footerText;
		IconUrl = footerIconUrl;
	}
}
public class EmbedImage
{
	public string Url;

	public EmbedImage(string imageUrl)
	{
		Url = imageUrl;
	}
}
public class EmbedThumbnail
{
	public string Url;

	public EmbedThumbnail(string thumbnailUrl)
	{
		Url = thumbnailUrl;
	}
}
public class EmbedAuthor
{
	public string Name;
	public string? Url;
	public string? IconUrl;

	public EmbedAuthor(string authorName, string? authorUrl = null, string? authorIconUrl = null)
	{
		Name = authorName;
		Url = authorUrl;
		IconUrl = authorIconUrl;
	}
}
public class EmbedField
{
	public string Name;
	public string Value;
	public bool? InLine;

	public EmbedField(string fieldName, string fieldValue, bool? fieldInLine = null)
	{
		Name = fieldName;
		Value = fieldValue;
		InLine = fieldInLine;
	}
}
