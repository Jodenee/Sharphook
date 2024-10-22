using Sharphook.ResponseObjects;

namespace Sharphook;

public class EmbedField
{
	public string Name { get; set; }
	public string Value { get; set; }
	public bool? InLine { get; set; }

	public EmbedField(string fieldName, string fieldValue, bool? fieldInLine = null)
	{
		Name = fieldName;
		Value = fieldValue;
		InLine = fieldInLine;
	}

	internal EmbedField(EmbedFieldObject embedFieldObject)
	{
		Name = embedFieldObject.Name;
		Value = embedFieldObject.Value;
		InLine = embedFieldObject.Inline;
	}
}
