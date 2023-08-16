namespace App;

public static class DateTimeConverter
{
    public static DateTime DateTimeConvert(double UNIXTimestamp)
    {
        return DateTime.UnixEpoch.AddMilliseconds(UNIXTimestamp);
    }
}