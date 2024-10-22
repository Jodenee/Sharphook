namespace Sharphook.Utility.Enums;

[Flags]
public enum MessageFlag : int
{
	SuppressEmbeds = 1 << 2,
	SuppressNotifications = 1 << 12
}
