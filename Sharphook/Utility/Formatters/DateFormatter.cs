namespace Sharphook.Utility.Formatters;

public static class DateFormatter
{
    public static string RelativeTime(DateTimeOffset datetimeOffset) => $"<t:{datetimeOffset.ToUnixTimeSeconds()}:R>";
    public static string RelativeTime(long unixSeconds) => $"<t:{unixSeconds}:R>";


    public static string LongDate(DateTimeOffset datetimeOffset) => $"<t:{datetimeOffset.ToUnixTimeSeconds()}:D>";
    public static string LongDate(long unixSeconds) => $"<t:{unixSeconds}:D>";


    public static string ShortDate(DateTimeOffset datetimeOffset) => $"<t:{datetimeOffset.ToUnixTimeSeconds()}:d>";
    public static string ShortDate(long unixSeconds) => $"<t:{unixSeconds}:d>";


    public static string LongTime(DateTimeOffset datetimeOffset) => $"<t:{datetimeOffset.ToUnixTimeSeconds()}:T>";
    public static string LongTime(long unixSeconds) => $"<t:{unixSeconds}:T>";


    public static string ShortTime(DateTimeOffset datetimeOffset) => $"<t:{datetimeOffset.ToUnixTimeSeconds()}:t>";
    public static string ShortTime(long unixSeconds) => $"<t:{unixSeconds}:t>";


    public static string LongDateTime(DateTimeOffset datetimeOffset) => $"<t:{datetimeOffset.ToUnixTimeSeconds()}:F>";
    public static string LongDateTime(long unixSeconds) => $"<t:{unixSeconds}:F>";


    public static string ShortDateTime(DateTimeOffset datetimeOffset) => $"<t:{datetimeOffset.ToUnixTimeSeconds()}:f>";
    public static string ShortDateTime(long unixSeconds) => $"<t:{unixSeconds}:f>";
}
