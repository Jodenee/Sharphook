using Sharphook.Utility.Enums;

namespace Sharphook;

public class CreatePoll
{
    public PollMedia Question { get; set; }
    public List<PollAnswer> Answers { get; set; }
    public int Duration { get; set; }
    public bool AllowMultipleChoice { get; set; }
    public PollLayoutType LayoutType { get; set; }

    public CreatePoll(
        string question, 
        List<PollAnswer> answers,
		int duration,
		bool allowMultipleChoice = false,
		PollLayoutType layoutType = PollLayoutType.DEFAULT)
    {
        Question = new PollMedia(question);
        Answers = answers;
		Duration = duration;
        AllowMultipleChoice = allowMultipleChoice;
        LayoutType = layoutType;
    }
}
