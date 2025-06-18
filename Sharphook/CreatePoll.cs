using Sharphook.Utility.Enums;

namespace Sharphook;

public class CreatePoll
{
    public PollMedia Question { get; set; }
    public PollAnswer[] Answers { get; set; }
    public int Duration { get; set; }
    public bool AllowMultipleChoice { get; set; }
    public PollLayoutType LayoutType { get; set; }

    public CreatePoll(
        string question,
        PollAnswer[] answers,
        int duration,
		bool allowMultipleChoice = false,
		PollLayoutType layoutType = PollLayoutType.DEFAULT)
    {
        if (answers.Length > 10) 
            throw new ArgumentException("A poll cannot have more than 10 answers.", nameof(answers));
        else if (answers.Length == 0)
            throw new ArgumentException("A poll must have at least 1 answer.", nameof(answers));

        Question = new PollMedia(question);
        Answers = answers;
		Duration = duration;
        AllowMultipleChoice = allowMultipleChoice;
        LayoutType = layoutType;
    }
}
