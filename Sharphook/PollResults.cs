using Sharphook.ResponseModels;

namespace Sharphook;

public class PollResults
{
	public bool IsFinalized { get; }
	public List<PollResult> Results { get; } = new List<PollResult>();

	internal PollResults(PollResultsObject pollResultsObject)
	{
		IsFinalized = pollResultsObject.IsFinalized;

		foreach (PollResultObject result in pollResultsObject.Results)
			Results.Add(new PollResult(result));
	}
}
