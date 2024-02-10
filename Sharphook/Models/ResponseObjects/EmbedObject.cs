#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Sharphook.DataTypes;
using System.Text.Json.Serialization;

namespace Sharphook.Models.ResponseObjects
{
    public class EmbedObject
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
        public EmbedObjectFooter? Footer { get; set; }

        [JsonPropertyName("image")]
        public EmbedObjectImage? Image { get; set; }

        [JsonPropertyName("thumbnail")]
        public EmbedObjectThumbnail? Thumbnail { get; set; }

        [JsonPropertyName("author")]
        public EmbedObjectAuthor? Author { get; set; }

        [JsonPropertyName("fields")]
        public List<EmbedObjectField>? Fields { get; set; }

        internal Embed ToEmbed()
        {
            Embed embed = new Embed(Title, Description, Url);

            if (Color != null)
            {
                embed.Color = new SharphookColor((uint)Color);
            }

            if (Timestamp != null)
            {
                embed.Timestamp = DateTime.Parse(Timestamp);
            }

            if (Footer != null)
            {
                embed.Footer = new EmbedFooter(Footer.Text, Footer.IconUrl);
            }

            if (Image != null)
            {
                embed.Image = new EmbedImage(Image.Url);
            }

            if (Thumbnail != null)
            {
                embed.Thumbnail = new EmbedThumbnail(Thumbnail.Url);
            }

            if (Author != null)
            {
                embed.Author = new EmbedAuthor(Author.Name, Author.Url, Author.IconUrl);
            }

            if (Fields != null)
            {
                foreach (EmbedObjectField fieldObject in Fields)
                {
                    embed.Fields.Add(new EmbedField(fieldObject.Name, fieldObject.Value, fieldObject.Inline));
                }
            }

            return embed;
        }
    }

    public class EmbedObjectFooter
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; set; }
    }

    public class EmbedObjectImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class EmbedObjectThumbnail
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class EmbedObjectAuthor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; set; }
    }

    public class EmbedObjectField
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("inline")]
        public bool? Inline { get; set; }
    }
}