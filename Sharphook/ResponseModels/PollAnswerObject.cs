#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

internal sealed record PollAnswerObject
{
    [JsonPropertyName("answer_id")]
    public int Id { get; set; }

    [JsonPropertyName("poll_media")]
    public PollMediaObject Media { get; set; }
}
