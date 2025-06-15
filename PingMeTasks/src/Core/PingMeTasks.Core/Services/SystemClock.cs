using PingMeTasks.Core.Interfaces.Common;

namespace PingMeTasks.Core.Services
{
    public class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public DateTimeOffset Now => DateTimeOffset.Now;

        public DateTimeOffset GetTimeInTimeZone(string timeZoneId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var utcNow = DateTime.UtcNow;
            return TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZone);
        }
    }
}
