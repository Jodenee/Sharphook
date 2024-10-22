using Sharphook.ResponseObjects;

namespace Sharphook;

public sealed class EmbedImage
{
	public string Url { get; set; }
	public string? ProxiedUrl { get; private set; }
	public int? Width { get; private set; }
	public int? Height { get; private set; }

	public EmbedImage(string imageUrl)
	{
		Url = imageUrl;
	}

	internal EmbedImage(EmbedImageObject embedImageObject)
	{
		Url = embedImageObject.Url;
		ProxiedUrl = embedImageObject.ProxiedUrl;
		Width = embedImageObject.Width;
		Height = embedImageObject.Hieght;
	}
}
