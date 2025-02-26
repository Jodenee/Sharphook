using Sharphook.ResponseModels;
using System.Text.Json.Serialization;

namespace Sharphook;

public class PollAnswer
{
	public int? Id { get; }
	public PollMedia Content { get; }

	internal PollAnswer(PollAnswerObject pollAnswerObject)
	{
		Id = pollAnswerObject.Id;
		Content = new PollMedia(pollAnswerObject.Media);
	}

	public PollAnswer(string text, PollEmoji? pollEmoji = null)
	{
		Content = new PollMedia(text, pollEmoji);
	}
}
