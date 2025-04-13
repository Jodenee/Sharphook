using Sharphook.ResponseModels;

namespace Sharphook;

public class PollAnswerCount
{
	public int AnswerId { get; }
	public int Votes { get; }
	public bool IVoted { get; }

	internal PollAnswerCount(PollAnswerCountObject pollResultObject)
	{
		AnswerId = pollResultObject.AnswerId;
		Votes = pollResultObject.Votes;
		IVoted = pollResultObject.IVoted;
	}
}
