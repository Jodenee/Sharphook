#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

internal sealed record PollResultsObject
{
    [JsonPropertyName("is_finalized")]
    public bool IsFinalized { get; set; }

    [JsonPropertyName("answer_counts")]
    public List<PollResultObject> Results { get; set; }
}