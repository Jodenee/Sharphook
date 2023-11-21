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
