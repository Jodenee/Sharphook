using Sharphook.ResponseModels;
using System.Text.Json.Serialization;

namespace Sharphook;

public class PollMedia
{
    public string Text { get; set; }
    public PollEmoji? Emoji { get; set; }

    internal PollMedia(PollMediaObject pollMediaObject)
    {
        Text = pollMediaObject.Text;
        Emoji = pollMediaObject.Emoji != null ? new PollEmoji(pollMediaObject.Emoji) : null;
    }

    public PollMedia(string text, PollEmoji? pollEmoji = null)
    {
        Text = text;
        Emoji = pollEmoji;
    }
}
