namespace Sharphook.Utility.Formatters;

public static class EmojiFormatter
{
    public static string Emoji(string emojiName) => $":{emojiName}:";

    public static string CustomEmoji(string emojiName, ulong emojiId) => $"<:{emojiName}:{emojiId}>";

    public static string AnimatedEmoji(string emojiName, ulong emojiId) => $"<a:{emojiName}:{emojiId}>";
}
