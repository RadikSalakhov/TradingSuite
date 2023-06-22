namespace Utilities
{
    public static class CommonUtilities
    {
        public static string GetDefaultDisplayFormat(decimal value)
        {
            if (value >= 1000m)
                return "0";
            else if (value >= 100m)
                return "0.0";
            else if (value >= 10m)
                return "0.00";
            else if (value >= 1m)
                return "0.000";
            else
                return "0.0000";
        }

        public static DateTime TruncateMillisecods(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
        }
    }
}