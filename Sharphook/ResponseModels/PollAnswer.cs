using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

public class PollAnswer
{
    public int Id { get; }
    public PollMedia Content { get; }

    internal PollAnswer(PollAnswerObject pollAnswerObject)
    {
        Id = pollAnswerObject.Id;
        Content = new PollMedia(pollAnswerObject.Media);
    }

    public PollAnswer(int id, string text, PollEmoji pollEmoji)
    {
        Id = id;
        Content = new PollMedia(text, pollEmoji);
    }
}
