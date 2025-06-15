
namespace PingMeTasks.Core.Interfaces.Common
{
    public interface IClock
    {
        /// <summary>
        /// Возвращает текущее время в формате UTC
        /// </summary>
        DateTime UtcNow { get; }

        /// <summary>
        /// Возвращает текущее время с учётом временной зоны
        /// </summary>
        DateTimeOffset Now { get; }

        /// <summary>
        /// Получить текущее время в конкретной временной зоне
        /// </summary>
        DateTimeOffset GetTimeInTimeZone(string timeZoneId);
    }
}
