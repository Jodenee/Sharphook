namespace Sharphook.Utility.Formatters;

public static class DateFormatter
{
    public static string RelativeTime(DateTimeOffset datetimeOffset)
    {
        return $"<t:{datetimeOffset.ToUnixTimeSeconds()}:R>";
    }
    public static string RelativeTime(long unixSeconds)
    {
        return $"<t:{unixSeconds}:R>";
    }

    public static string LongDate(DateTimeOffset datetimeOffset)
    {
        return $"<t:{datetimeOffset.ToUnixTimeSeconds()}:D>";
    }
    public static string LongDate(long unixSeconds)
    {
        return $"<t:{unixSeconds}:D>";
    }

    public static string ShortDate(DateTimeOffset datetimeOffset)
    {
        return $"<t:{datetimeOffset.ToUnixTimeSeconds()}:d>";
    }
    public static string ShortDate(long unixSeconds)
    {
        return $"<t:{unixSeconds}:d>";
    }

    public static string LongTime(DateTimeOffset datetimeOffset)
    {
        return $"<t:{datetimeOffset.ToUnixTimeSeconds()}:T>";
    }
    public static string LongTime(long unixSeconds)
    {
        return $"<t:{unixSeconds}:T>";
    }

    public static string ShortTime(DateTimeOffset datetimeOffset)
    {
        return $"<t:{datetimeOffset.ToUnixTimeSeconds()}:t>";
    }
    public static string ShortTime(long unixSeconds)
    {
        return $"<t:{unixSeconds}:t>";
    }

    public static string LongDateTime(DateTimeOffset datetimeOffset)
    {
        return $"<t:{datetimeOffset.ToUnixTimeSeconds()}:F>";
    }
    public static string LongDateTime(long unixSeconds)
    {
        return $"<t:{unixSeconds}:F>";
    }

    public static string ShortDateTime(DateTimeOffset datetimeOffset)
    {
        return $"<t:{datetimeOffset.ToUnixTimeSeconds()}:f>";
    }
    public static string ShortDateTime(long unixSeconds)
    {
        return $"<t:{unixSeconds}:f>";
    }
}
