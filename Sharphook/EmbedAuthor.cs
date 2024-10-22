using Sharphook.ResponseObjects;

namespace Sharphook;

public sealed class EmbedAuthor
{
	public string Name { get; set; }
	public string? Url { get; set; }
	public string? IconUrl { get; set; }
	public string? ProxiedIconUrl { get; private set; }

	public EmbedAuthor(string authorName, string? authorUrl = null, string? authorIconUrl = null)
	{
		Name = authorName;
		Url = authorUrl;
		IconUrl = authorIconUrl;
	}

	internal EmbedAuthor(EmbedAuthorObject embedAuthorObject)
	{
		Name = embedAuthorObject.Name;
		Url = embedAuthorObject.Url;
		IconUrl = embedAuthorObject.IconUrl;
		ProxiedIconUrl = embedAuthorObject.ProxiedIconUrl;
	}
}
