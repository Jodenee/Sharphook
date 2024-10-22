namespace Sharphook.Utility.Enums;

[Flags]
public enum AllowedMentionType : int
{
	Roles = 1 << 0,
	Users = 1 << 1,
	Everyone = 1 << 2,
}
