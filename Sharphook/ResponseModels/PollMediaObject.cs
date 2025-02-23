#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

internal sealed record PollMediaObject
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("emoji")]
    public PollEmojiObject? Emoji { get; set; }

    [JsonConstructor]
    public PollMediaObject() { }

    internal PollMediaObject(PollMedia pollMedia)
    {
        Text = pollMedia.Text;
        Emoji = pollMedia.Emoji != null ? new PollEmojiObject(pollMedia.Emoji) : null;
    }
}
