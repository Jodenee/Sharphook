using Sharphook.Models.ResponseObjects.PartialObjects;

namespace Sharphook.Models.ResponseObjects
{
    public class WebhookObject
    {
        public Int16 type { get; set; }
        public string id { get; set; }
        public string? application_id { get;  set; }
        public string token { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string guild_id { get; set; }
        public string channel_id { get; set; }
        public string? avatar { get; set; }
        public PartialUserObject? user { get; set; }
    }
}
