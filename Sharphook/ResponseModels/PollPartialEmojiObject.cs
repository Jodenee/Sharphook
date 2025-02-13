#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

internal sealed record PollPartialEmojiObject
{
    [JsonPropertyName("id")]
    public ulong? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}