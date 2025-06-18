using Sharphook.ResponseModels;
using Sharphook.Utility.Enums;

namespace Sharphook;

public class Poll
{
    private readonly WebhookClient _client;

    public PollMedia Question { get; set; }
    public PollAnswer[] Answers { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool AllowMultipleChoice { get; set; }
    public PollLayoutType LayoutType { get; set; }
    public PollResults? Results { get; set; }

    internal Poll(WebhookClient client, PollObject pollObject)
    {
		_client = client;
		Question = new PollMedia(pollObject.Question);
        Answers = new PollAnswer[pollObject.Answers.Length];
        ExpiresAt = DateTime.Parse(pollObject.Expiry);
        AllowMultipleChoice = pollObject.AllowMultiselect;
        LayoutType = (PollLayoutType) pollObject.LayoutType;
        Results = pollObject.Results != null ? new PollResults(pollObject.Results) : null;

        for (int i = 0; i < pollObject.Answers.Length; i++)
            Answers[i] = new PollAnswer(pollObject.Answers[i]);
    }
}
