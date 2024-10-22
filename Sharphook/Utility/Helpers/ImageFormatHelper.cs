using Sharphook.Utility.Enums;
using System.ComponentModel;

namespace Sharphook.Utility.Helpers;

internal static class ImageFormatHelper
{
	public static async Task<string> ToDataURIAsync(string filePath, ImageFormat imageFormat)
	{
		byte[] imageByteArray = await File.ReadAllBytesAsync(filePath);
		string base64ImageData = Convert.ToBase64String(imageByteArray);
		string contentType = "";

		switch (imageFormat)
		{
			case ImageFormat.Jpeg:
				contentType = "image/jpeg";
				break;
			case ImageFormat.Png:
				contentType = "image/png";
				break;
			case ImageFormat.WebP:
				contentType = "image/webp";
				break;
			case ImageFormat.Gif:
				contentType = "image/gif";
				break;
		};

		return $"data:{contentType};base64,{base64ImageData}";
	}

	public static string ToExtension(ImageFormat imageFormat)
	{
		return imageFormat switch
		{
			ImageFormat.Jpeg => ".jpeg",
			ImageFormat.Png => ".png",
			ImageFormat.WebP => ".webp",
			ImageFormat.Gif => ".gif",
			ImageFormat.Lottie => ".json",
			_ => throw new InvalidEnumArgumentException()
		};
	}

	public static ImageFormat Parse(string extension)
	{
		return extension switch
		{
			".jpg" => ImageFormat.Jpeg,
			".jpeg" => ImageFormat.Jpeg,
			".jpe" => ImageFormat.Jpeg,
			".jif" => ImageFormat.Jpeg,
			".jfif" => ImageFormat.Jpeg,
			".jfi" => ImageFormat.Jpeg,

			".png" => ImageFormat.Png,
			".webp" => ImageFormat.WebP,
			".gif" => ImageFormat.Gif,
			".json" => ImageFormat.Lottie,
			_ => throw new Exception($"Could not parse extension {extension}")
		};
	}
}
