using Sharphook.ResponseModels;
using Sharphook.Utility.Enums;

namespace Sharphook;

public class Poll
{
    private readonly WebhookClient _client;

    public PollMedia Question { get; set; }
    public List<PollAnswer> Answers { get; set; } = new List<PollAnswer>();
    public DateTime ExpiresAt { get; set; }
    public bool AllowMultipleChoice { get; set; }
    public PollLayoutType LayoutType { get; set; }
    public PollResults? Results { get; set; }

    internal Poll(WebhookClient client, PollObject pollObject)
    {
		_client = client;
		Question = new PollMedia(pollObject.Question);
        ExpiresAt = DateTime.Parse(pollObject.Expiry);
        AllowMultipleChoice = pollObject.AllowMultiselect;
        LayoutType = (PollLayoutType) pollObject.LayoutType;
        Results = pollObject.Results != null ? new PollResults(pollObject.Results) : null;

        foreach (PollAnswerObject answerObject in pollObject.Answers)
            Answers.Add(new PollAnswer(answerObject));
    }
}
