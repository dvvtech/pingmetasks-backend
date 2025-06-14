
namespace PingMeTasks.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
        {
            var result = from.AddDays((7 + dayOfWeek - from.DayOfWeek) % 7);
            return result == from ? result.AddDays(7) : result;
        }

        public static DateTime SetDay(this DateTime date, int day)
        {
            return new DateTime(date.Year, date.Month, Math.Min(day, DateTime.DaysInMonth(date.Year, date.Month)),
                                date.Hour, date.Minute, date.Second);
        }

        public static DateTime SetMonth(this DateTime date, int month)
        {
            return new DateTime(date.Year, Math.Min(month, 12), date.Day,
                                date.Hour, date.Minute, date.Second);
        }
    }
}
