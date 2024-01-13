using Sharphook.DataTypes;

namespace Sharphook.Models.ResponseObjects
{
    public class EmbedObject
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public string? url { get; set; }
        public uint? color { get; set; }
        public string? timestamp { get; set; }
        public EmbedObjectFooter? footer { get; set; }
        public EmbedObjectImage? image { get; set; }
        public EmbedObjectThumbnail? thumbnail { get; set; }
        public EmbedObjectAuthor? author { get; set; }
        public List<EmbedObjectField>? fields { get; set; }

        internal Embed _ToEmbed()
        {
            Embed embed = new Embed(title, description, url);

            if (color != null)
            {
                embed.Color = new SharphookColor((uint)color);
            }

            if (timestamp != null)
            {
                embed.Timestamp = DateTime.Parse(timestamp);
            }

            if (footer != null)
            {
                embed.Footer = new EmbedFooter(footer.text, footer.icon_url);
            }

            if (image != null)
            {
                embed.Image = new EmbedImage(image.url);
            }

            if (thumbnail != null)
            {
                embed.Thumbnail = new EmbedThumbnail(thumbnail.url);
            }

            if (author != null)
            {
                embed.Author = new EmbedAuthor(author.name, author.url, author.icon_url);
            }

            if (fields != null)
            {
                foreach (EmbedObjectField fieldObject in fields)
                {
                    embed.Fields.Add(new EmbedField(fieldObject.name, fieldObject.value, fieldObject.inline));
                }
            }

            return embed;
        }
    }

    public class EmbedObjectFooter
    {
        public string text { get; set; }
        public string? icon_url { get; set; }
    }

    public class EmbedObjectImage
    {
        public string url { get; set; }
    }

    public class EmbedObjectThumbnail
    {
        public string url { get; set; }
    }

    public class EmbedObjectAuthor
    {
        public string name { get; set; }
        public string? url { get; set; }
        public string? icon_url { get; set; }
    }

    public class EmbedObjectField
    {
        public string name { get; set; }
        public string value { get; set; }
        public bool? inline { get; set; }
    }
}
