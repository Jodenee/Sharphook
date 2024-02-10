#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Sharphook.Models.ResponseObjects.PartialObjects;
using System.Text.Json.Serialization;

namespace Sharphook.Models.ResponseObjects
{
    public class MessageObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("author")]
        public PartialUserObject Author { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("timestamp")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("type")]
        public byte Type { get; set; }

        [JsonPropertyName("embeds")]
        public List<EmbedObject> Embeds { get; set; }

        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }

        [JsonPropertyName("mention_everyone")]
        public bool MentionEveryone { get; set; }

        [JsonPropertyName("tts")]
        public bool IsTTS { get; set; }

        [JsonPropertyName("attachments")]
        public List<AttachmentObject> Attachments { get; set; }

        [JsonPropertyName("flags")]
        public int Flags { get; set; }

        [JsonPropertyName("webhook_id")]
        public string WebhookId { get; set; }
    }
}
