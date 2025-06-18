using Sharphook.ResponseModels;

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
        Id = pollEmojiObject.Id != null ? Convert.ToUInt64(pollEmojiObject.Id) : null;
        Name = pollEmojiObject.Name;
    }
}
