using Sharphook.ResponseModels;

namespace Sharphook;

public class PollResult
{
	public int Id { get; }
	public int Votes { get; }
	public bool IVoted { get; }

	internal PollResult(PollResultObject pollResultObject)
	{
		Id = pollResultObject.Id;
		Votes = pollResultObject.Votes;
		IVoted = pollResultObject.IVoted;
	}
}
