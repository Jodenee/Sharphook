#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

internal class PollResultObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("count")]
    public int Votes { get; set; }

    [JsonPropertyName("me_voted")]
    public bool IVoted { get; set; }
}
