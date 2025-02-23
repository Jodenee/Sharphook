using Sharphook.ResponseModels;
using Sharphook.Utility.Enums;

namespace Sharphook;

public class Poll
{
    public PollMedia Question { get; set; }
    public List<PollAnswer> Answers { get; set; } = new List<PollAnswer>();
    public DateTime ExpiresAt { get; set; }
    public bool AllowMultipleChoice { get; set; }
    public PollLayoutType LayoutType { get; set; }
    public PollResults? Results { get; set; }

    public Poll(PollMedia question, 
        List<PollAnswer> answers, 
        TimeSpan expiresIn, 
        bool allowMultipleChoice, 
        PollLayoutType? layoutType = null)
    {
        Question = question;
        Answers = answers;
        ExpiresAt = DateTime.Now + expiresIn;
        AllowMultipleChoice = allowMultipleChoice;
        LayoutType = layoutType ?? PollLayoutType.DEFAULT;
    }

    internal Poll(PollObject pollObject)
    {
        Question = new PollMedia(pollObject.Question);
        ExpiresAt = DateTime.Parse(pollObject.Expiry);
        AllowMultipleChoice = pollObject.AllowMultiselect;
        LayoutType = (PollLayoutType) pollObject.LayoutType;
        Results = pollObject.Results != null ? new PollResults(pollObject.Results) : null;

        foreach (PollAnswerObject answerObject in pollObject.Answers)
            Answers.Add(new PollAnswer(answerObject));
    }
}
