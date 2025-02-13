#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

internal sealed record PollMediaObject
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("emoji")]
    public PollPartialEmojiObject? Emoji { get; set; }
}
