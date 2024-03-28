using Sharphook.Utility.Enums;
using System.ComponentModel;

namespace Sharphook.Utility.Formatters;

internal static class ImageFormatParser
{
    public static async Task<string> ToDataURIAsync(string avatarFilePath, ImageFormat imageFormat)
    {
        byte[] imageByteArray = await File.ReadAllBytesAsync(avatarFilePath);
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
}
