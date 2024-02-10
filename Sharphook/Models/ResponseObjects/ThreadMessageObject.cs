using System.Text.Json.Serialization;

namespace Sharphook.Models.ResponseObjects
{
    public class ThreadMessageObject : MessageObject
    {
        [JsonPropertyName("position")]
        public uint Position { get; set; }
    }
}
