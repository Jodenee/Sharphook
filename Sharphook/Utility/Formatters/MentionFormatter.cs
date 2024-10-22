namespace Sharphook.Utility.Formatters;

public static class MentionFormatter
{
	public static string Everyone = "@everyone";
	public static string Here = "@here";

	public static string User(ulong userId)
		=> $"<@{userId}>";

	public static string Role(ulong roleId)
		=> $"<@&{roleId}>";

	public static string Channel(ulong channelId)
		=> $"<#{channelId}>";
}