#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace Sharphook.ResponseModels;

internal sealed record CreatePollObject
{
	[JsonPropertyName("question")]
	public PollMediaObject Question { get; set; }

	[JsonPropertyName("answers")]
	public List<PollAnswerObject> Answers { get; set; } = new List<PollAnswerObject>();

	[JsonPropertyName("duration")]
	public int Duration { get; set; }

	[JsonPropertyName("allow_multiselect")]
	public bool AllowMultiselect { get; set; }

	[JsonPropertyName("layout_type")]
	public int LayoutType { get; set; }

	internal CreatePollObject(CreatePoll createPoll)
	{
		Question = new PollMediaObject(createPoll.Question);
		Duration = (int) createPoll.Duration.TotalHours;
		AllowMultiselect = createPoll.AllowMultipleChoice;
		LayoutType = (int) createPoll.LayoutType;

		foreach (PollAnswer answer in createPoll.Answers)
			Answers.Add(new PollAnswerObject(answer));
	}
}
