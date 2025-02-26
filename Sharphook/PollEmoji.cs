using Sharphook.ResponseModels;
using Sharphook.ResponseObjects;
using System.Text.Json.Serialization;

namespace Sharphook;

public class PollEmoji
{
    public ulong? Id { get; }
    public string? Name { get; }

    public PollEmoji(ulong emojiId)
    {
        Id = emojiId;
    }

    public PollEmoji(string emojiName)
    {
        Name = emojiName;
    }

    internal PollEmoji(PollEmojiObject pollEmojiObject)
    {
        Id = pollEmojiObject.Id;
        Name = pollEmojiObject.Name;
    }
}
