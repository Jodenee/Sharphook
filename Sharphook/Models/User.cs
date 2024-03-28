using Sharphook.Models.ResponseObjects;
using Sharphook.Utility.Enums;
using Sharphook.Utility.Formatters;

namespace Sharphook.Models;

public class User
{
    WebhookClient Client { get; set; }
    public ulong Id { get; private set; }
    public string Username { get; private set; }
    public string? GlobalName { get; private set; }
    public string? AvatarHash { get; private set; }
    public PublicUserFlags PublicFlags { get; private set; }
    public bool IsBot { get; private set; }

    public string Mention
    {
        get => $"<@{Id}>";
    }

    public User(WebhookClient client, UserObject userObject)
    {
        Client = client;
        Id = Convert.ToUInt64(userObject.Id);
        Username = userObject.Username;
        GlobalName = userObject.GlobalName;
        AvatarHash = userObject.Avatar;
        PublicFlags = new PublicUserFlags(userObject.PublicFlags);
        IsBot = userObject.IsBot ?? false;
    }

    public string? GetAvatarUrl()
    {
        if (AvatarHash == null) { return null; }

        return $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}.png";
    }

    public string? GetAvatarUrl(int size)
    {
        if (AvatarHash == null) { return null; }

        return $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}.png?size={size}";
    }

    public string? GetAvatarUrl(ImageFormat imageFormat = ImageFormat.Png)
    {
        if (AvatarHash == null) { return null; }

        return $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}{ImageFormatParser.ToExtension(imageFormat)}";
    }

    public string? GetAvatarUrl(int size, ImageFormat imageFormat = ImageFormat.Png)
    {
        if (AvatarHash == null) { return null; }

        return $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}{ImageFormatParser.ToExtension(imageFormat)}?size={size}";
    }

    public bool HasAnimatedAvatar()
    {
        if (AvatarHash == null) { return false; }

        return AvatarHash.StartsWith("a_");
    }
}
