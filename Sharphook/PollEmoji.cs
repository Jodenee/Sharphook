using Sharphook.ResponseModels;
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
        if (pollEmojiObject.Id != null)
            Id = pollEmojiObject.Id;
        else 
            Name = pollEmojiObject.Name;
    }
}
