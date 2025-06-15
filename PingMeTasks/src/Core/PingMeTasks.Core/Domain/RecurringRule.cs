using PingMeTasks.Core.Extensions;
using PingMeTasks.Core.Interfaces.Common;
using System.Data;
using System.Threading;

namespace PingMeTasks.Core.Domain
{
    /// <summary>
    /// Описание повторения задачи
    /// </summary>
    public class RecurringRule
    {
        public int Id { get; set; }

        public RepeatType Type { get; set; }

        /// <summary>
        /// // Каждые N дней/недель/месяцев
        /// </summary>
        public int Interval { get; set; } = 1;



        /// <summary>
        /// Monday,Wednesday" (для Weekly)
        /// </summary>
        public DayOfWeek? DayOfWeek { get; set; }

        /// <summary>
        /// для ежемесячного
        /// </summary>
        public int? DayOfMonth { get; set; }

        /// <summary>
        /// для годового
        /// </summary>
        public int? MonthOfYear { get; set; }



        /// <summary>
        /// Дата первого события
        /// </summary>
        public DateTime StartDateUtc { get; set; } 

        /// <summary>
        /// До какой даты повторять
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Максимальное число повторений
        /// </summary>
        public int? MaxOccurrences { get; set; }



        // Для кастомных правил типа "2 дня да / 2 дня нет"
        public int ActiveDays { get; set; } = 1;
        public int RestDays { get; set; } = 1;


        // Внешний ключ
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }

        /// <summary>
        /// Получить следующие даты, начиная с текущего времени (UTC)
        /// </summary>
        /// <param name="clock"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        public List<DateTime> GetUpcomingOccurrences(IClock clock, int maxCount = 10)
        {
            // Здесь реализуется логика генерации дат по правилам
            var occurrences = new List<DateTime>();
            var currentDate = GetCurrentStartDate(clock);

            int count = 0;
            while ((MaxOccurrences == null || count < MaxOccurrences) &&
                   (EndDateUtc == null || currentDate <= EndDateUtc) &&
                   count < maxCount)
            {
                occurrences.Add(currentDate);
                currentDate = CalculateNextDate(currentDate);
                count++;
            }

            return occurrences;
        }

        private DateTime GetCurrentStartDate(IClock clock)
        {
            var now = clock.UtcNow;            

            // Если дата начала ещё не наступила
            if (now < StartDateUtc)
                return StartDateUtc;

            // Ищем последнюю прошедшую дату, чтобы начать с новой
            var lastOccurrence = FindLastOccurrence(now);
            return lastOccurrence.HasValue ? CalculateNextDate(lastOccurrence.Value) : StartDateUtc;
        }

        private DateTime? FindLastOccurrence(DateTime before)
        {
            var result = StartDateUtc;
            while (true)
            {
                var next = CalculateNextDate(result);
                if (next >= before)
                    break;
                result = next;
            }
            return result;
        }

        private DateTime CalculateNextDate(DateTime current)
        {
            return Type switch
            {
                RepeatType.Daily => current.AddDays(Interval),
                RepeatType.Weekly when DayOfWeek.HasValue =>
                    current.Next(DayOfWeek.Value).AddDays(7 * (Interval - 1)),
                RepeatType.Monthly when DayOfMonth.HasValue =>
                    current.AddMonths(Interval).SetDay(DayOfMonth.Value),
                RepeatType.Yearly when MonthOfYear.HasValue && DayOfMonth.HasValue =>
                    current.AddYears(Interval).SetMonth(MonthOfYear.Value).SetDay(DayOfMonth.Value),
                RepeatType.CustomDaysPattern => GetNextCustomPatternDate(current),
                _ => current.AddDays(Interval) // fallback
            };
        }

        private DateTime GetNextCustomPatternDate(DateTime current)
        {
            var activeDays = ActiveDays;
            var restDays = RestDays;
            var cycleLength = activeDays + restDays;

            // Найдём начало текущего цикла
            var daysSinceStart = (current.Date - StartDateUtc.Date).Days;

            if (daysSinceStart < 0)
                return StartDateUtc;

            if (daysSinceStart % cycleLength < activeDays)
            {
                // Текущий день внутри активной фазы
                return current.AddDays(1);
            }
            else
            {
                // Мы в фазе отдыха — перескакиваем на начало следующей активной фазы
                var daysToSkip = cycleLength - (daysSinceStart % cycleLength);
                return current.AddDays(daysToSkip);
            }
        }
    }

    public enum RepeatType { Daily, Weekly, Monthly, Yearly, CustomDaysPattern }

    //RecurrenceType
}
