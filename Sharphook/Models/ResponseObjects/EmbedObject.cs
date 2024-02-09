using Sharphook.DataTypes;

#pragma warning disable IDE1006 // Naming Styles

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

        internal Embed ToEmbed()
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
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string text { get; set; }
        public string? icon_url { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
    }

    public class EmbedObjectImage
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string url { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
    }

    public class EmbedObjectThumbnail
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string url { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
    }

    public class EmbedObjectAuthor
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string name { get; set; }
        public string? url { get; set; }
        public string? icon_url { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
    }

    public class EmbedObjectField
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string name { get; set; }
        public string value { get; set; }
        public bool? inline { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
    }
}
