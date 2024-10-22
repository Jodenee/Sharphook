
using Sharphook.ResponseObjects;

namespace Sharphook;

public class EmbedThumbnail
{
	public string Url { get; set; }
	public string? ProxiedUrl { get; private set; }
	public int? Width { get; private set; }
	public int? Height { get; private set; }

	public EmbedThumbnail(string thumbnailUrl)
	{
		Url = thumbnailUrl;
	}

	internal EmbedThumbnail(EmbedThumbnailObject embedThumbnailObject)
	{
		Url = embedThumbnailObject.Url;
		ProxiedUrl = embedThumbnailObject.ProxiedUrl;
		Width = embedThumbnailObject.Width;
		Height = embedThumbnailObject.Hieght;
	}
}
