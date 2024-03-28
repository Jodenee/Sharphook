#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.Models.ResponseObjects;

public class EditedThreadMessageObject : ThreadMessageObject
{
    [JsonPropertyName("edited_timestamp")]
    public string EditedAt { get; set; }
}