#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE1006 // Naming Styles

namespace Sharphook.Models.ResponseObjects
{
    public class AttachmentObject
    {
        public ulong id { get; set; }
        public string filename { get; set; }
        public string? description { get; set; }
        public string? content_type { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string proxy_url { get; set; }
        public int? height { get; set; }
        public int? width { get; set; }
        public int? flags { get; set; }
    }
}
