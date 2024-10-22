#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseObjects;

internal sealed record EmbedFieldObject
{
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("value")]
	public string Value { get; set; }

	[JsonPropertyName("inline")]
	public bool? Inline { get; set; }

	public EmbedFieldObject() { }

	internal EmbedFieldObject(EmbedField embedField)
	{
		Name = embedField.Name;
		Value = embedField.Value;
		Inline = embedField.InLine;
	}
}