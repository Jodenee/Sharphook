namespace Sharphook;

public sealed class OptionalEditMessageInfo
{
	public List<SharphookFile>? Files { get; set; }
	public AllowedMentions? AllowedMentions { get; set; }

	public OptionalEditMessageInfo(
		List<SharphookFile>? files = null,
		AllowedMentions? allowedMentions = null)
	{
		Files = files;
		AllowedMentions = allowedMentions;
	}
}
