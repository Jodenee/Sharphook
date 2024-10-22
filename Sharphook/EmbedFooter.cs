using Sharphook.ResponseObjects;

namespace Sharphook;

public class EmbedFooter
{
	public string Text { get; set; }
	public string? IconUrl { get; set; }
	public string? ProxiedIconUrl { get; private set; }

	public EmbedFooter(string footerText, string? footerIconUrl = null)
	{
		Text = footerText;
		IconUrl = footerIconUrl;
	}

	internal EmbedFooter(EmbedFooterObject embedFooterObject)
	{
		Text = embedFooterObject.Text;
		IconUrl = embedFooterObject.IconUrl;
		ProxiedIconUrl = embedFooterObject.ProxiedIconUrl;
	}
}
