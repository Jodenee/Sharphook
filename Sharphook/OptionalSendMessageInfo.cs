using Sharphook.Utility.Enums;

namespace Sharphook;

public class OptionalSendMessageInfo
{
	public bool? TTS { get; set; }
	public string? UsernameOverride { get; set; }
	public string? AvatarUrlOverride { get; set; }
	public MessageFlag? MessageFlags { get; set; }
	public string? ThreadName { get; set; }
	public List<SharphookFile>? Files { get; set; }
	public AllowedMentions? AllowedMentions { get; set; }
	public List<ulong>? ApplyTags { get; set; }

	public OptionalSendMessageInfo(
		bool? tts = null,
		string? usernameOverride = null,
		string? avatarUrlOverride = null,
		MessageFlag? messageFlags = null,
		string? threadName = null,
		List<SharphookFile>? files = null,
		AllowedMentions? allowedMentions = null,
		List<ulong>? applyTags = null)
	{
		TTS = tts;
		UsernameOverride = usernameOverride;
		AvatarUrlOverride = avatarUrlOverride;
		MessageFlags = messageFlags;
		ThreadName = threadName;
		Files = files;
		AllowedMentions = allowedMentions;
		ApplyTags = applyTags;
	}
}
