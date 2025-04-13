using Sharphook.ResponseModels;

namespace Sharphook;

public class PollResults
{
	public bool IsFinalized { get; }
	public List<PollAnswerCount> AnswerCounts { get; } = new List<PollAnswerCount>();

	internal PollResults(PollResultsObject pollResultsObject)
	{
		IsFinalized = pollResultsObject.IsFinalized;

		foreach (PollAnswerCountObject result in pollResultsObject.Results)
			AnswerCounts.Add(new PollAnswerCount(result));
	}
}
