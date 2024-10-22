#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseObjects;

internal sealed record EmbedFooterObject
{
	[JsonPropertyName("text")]
	public string Text { get; set; }

	[JsonPropertyName("icon_url")]
	public string? IconUrl { get; set; }

	[JsonPropertyName("proxy_icon_url")]
	public string? ProxiedIconUrl { get; set; }

	public EmbedFooterObject() { }

	internal EmbedFooterObject(EmbedFooter embedFooter)
	{
		Text = embedFooter.Text;
		IconUrl = embedFooter.IconUrl;
	}
}
