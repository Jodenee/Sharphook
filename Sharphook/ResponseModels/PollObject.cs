#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

internal sealed record PollObject
{
    [JsonPropertyName("question")]
    public PollMediaObject Question { get; set; }

    [JsonPropertyName("answers")]
    public List<PollAnswerObject> Answers { get; set; }

    [JsonPropertyName("expiry")]
    public string Expiry { get; set; }

    [JsonPropertyName("allow_multiselect")]
    public bool AllowMultiselect { get; set; }

    [JsonPropertyName("layout_type")]
    public string LayoutType { get; set; }

    [JsonPropertyName("results")]
    public PollResultsObject? Results { get; set; }
}
