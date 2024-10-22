using Sharphook.Utility.Enums;
using Sharphook.Utility.Helpers;
using System.Collections.Specialized;
using System.Web;

namespace Sharphook;

public sealed class Asset
{
	private readonly WebhookClient _client;

	public string Url { get; private set; }
	public bool Animated { get; private set; }
	public string Identifier { get; private set; }
	public ImageFormat Format { get; private set; }

	public Asset(WebhookClient client, string url, bool animated, string identifier, ImageFormat format)
	{
		_client = client;
		Url = url;
		Animated = animated;
		Identifier = identifier;
		Format = format;
	}

	public async Task<byte[]> ReadAsync() =>
		await _client.GetFromCDN(new Uri(Url));

	public async Task SaveAsync(string path, string? filename = null)
	{
		byte[] data = await ReadAsync();
		string filePath = Path.Combine(Path.GetFullPath(path), $"{filename ?? Identifier}{ImageFormatHelper.ToExtension(Format)}");

		await File.WriteAllBytesAsync(filePath, data);
	}

	public async Task SaveAsync(FileStream fileStream, bool resetPositionAfterWrite)
	{
		byte[] data = await ReadAsync();

		await fileStream.WriteAsync(data);
		if (resetPositionAfterWrite) fileStream.Position = 0;
	}

	internal static bool IsValidImageSize(int size)
		=> !Convert.ToBoolean(size & (size - 1)) && size <= 4096 && size >= 16;

	public Asset WithSize(int size)
	{
		if (!IsValidImageSize(size))
			throw new ArgumentException("Image/icon size must be power of 2 within [16, 4096].");

		string[] splitUrl = Url.Split('?', 2);
		NameValueCollection query = HttpUtility.ParseQueryString(splitUrl[1]);

		if (query.Get("size") != null)
			query.Remove("size");
		query.Set("size", size.ToString());

		string newUrl = splitUrl[0] + '?' + query.ToString();

		return new Asset(_client, newUrl, Animated, Identifier, Format);
	}

	public Asset WithFormat(ImageFormat format)
	{
		string newExtension = ImageFormatHelper.ToExtension(format);
		int fileExtensionIndex = Url.LastIndexOf('.');
		int queryStartIndex = Url.LastIndexOf('?');
		string newUrl = Url
			.Remove(fileExtensionIndex, queryStartIndex - fileExtensionIndex)
			.Insert(fileExtensionIndex, newExtension);

		return new Asset(_client, newUrl, Animated, Identifier, format);
	}

	internal static Asset FromAvatar(WebhookClient client, ulong UserId, string avatarHash)
	{
		bool animated = avatarHash.StartsWith("a_");
		string format = animated ? "gif" : "png";

		return new Asset(
			client,
			$"{client.BaseCDNUrl}/avatars/{UserId}/{avatarHash}.{format}?size=1024",
			animated,
			avatarHash,
			animated ? ImageFormat.Gif : ImageFormat.Png
		);
	}
}
