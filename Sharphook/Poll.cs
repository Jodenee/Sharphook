using Sharphook.ResponseModels;
using Sharphook.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sharphook;

public class Poll
{
    public PollMedia Question { get; set; }
    public List<PollAnswer> Answers { get; set; } = new List<PollAnswer>();
    public DateTime ExpiresAt { get; set; }
    public bool AllowMultipleChoice { get; set; }
    public PollLayoutType LayoutType { get; set; }
    public object? Results { get; set; }

    public Poll(PollMedia question, 
        List<PollAnswer> answers, 
        TimeSpan expiresIn, 
        bool allowMultipleChoice, 
        PollLayoutType layoutType)
    {
        Question = question;
        Answers = answers;
        ExpiresAt = DateTime.Now + expiresIn;
        AllowMultipleChoice = allowMultipleChoice;
    }

    internal Poll(PollObject pollObject)
    {
        Question = new PollMedia(pollObject.Question);
        ExpiresAt = DateTime.Parse(pollObject.Expiry);
        AllowMultipleChoice = pollObject.AllowMultiselect;
        LayoutType = Enum.Parse<PollLayoutType>(pollObject.LayoutType);
        Results = pollObject.Results;

        foreach (PollAnswerObject answerObject in pollObject.Answers)
            Answers.Add(new PollAnswer(answerObject));
    }
}
