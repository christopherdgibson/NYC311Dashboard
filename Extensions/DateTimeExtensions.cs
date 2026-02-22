namespace NYC311Dashboard.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? RoundToNearestHour(this DateTime? input)
        {
            if (input == null)
            {
                return null;
            }

            var target = (DateTime)input;
            var targetHours = target.Minute > 30 ? target.AddHours(1).AddMinutes(-target.Minute)
            : target.AddMinutes(-target.Minute).AddSeconds(-target.Second);
            return targetHours.AddMinutes(-targetHours.Minute).AddSeconds(-targetHours.Second);
        }

        public static DateTime? TruncateToHour(this DateTime? input)
        {
            if (input == null)
            {
                return null;
            }

            var target = (DateTime)input;
            return new DateTime(target.Year, target.Month, target.Day, target.Hour, 0, 0);
        }

        public static DateTime ToDateTime(this DateTime? input)
        {
            if (input == null)
            {
                return DateTime.MinValue;
            }

            return (DateTime)input;
        }

        public static string ToDateTimeHour(this DateTime? input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            return input.ToDateTime().ToString("dd MMM yyyy HH'h'");
        }
    }
}
