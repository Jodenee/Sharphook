namespace Sharphook.Utility.Formatters;

public static class MentionFormatter
{
	public static string MentionUser(ulong userId) => $"<@{userId}>";

	public static string MentionRole(ulong roleId) => $"<@&{roleId}>";

	public static string MentionChannel(ulong channelId) => $"<#{channelId}>";
}