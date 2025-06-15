using PingMeTasks.Core.Extensions;
using PingMeTasks.Core.Interfaces.Common;

namespace PingMeTasks.Core.Domain
{
    /// <summary>
    /// Описание повторения задачи
    /// </summary>
    public class RecurringRule
    {
        public int Id { get; set; }
        public RepeatType Type { get; set; }
        public int Interval { get; set; } = 1; // Каждые N дней/недель/месяцев
        public DayOfWeek? DayOfWeek { get; set; } // "Monday,Wednesday" (для Weekly)
        public int? DayOfMonth { get; set; } //для ежемесячного

        public int? MonthOfYear { get; set; }     // для годового

        /// <summary>
        /// Дата первого события
        /// </summary>
        public DateTime StartDate { get; set; } 

        /// <summary>
        /// До какой даты повторять
        /// </summary>
        public DateTime? EndDate { get; set; }

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

        public List<DateTime> GetUpcomingOccurrences(IClock clock, int maxCount = 10)
        {
            // Здесь реализуется логика генерации дат по правилам
            var occurrences = new List<DateTime>();
            var currentDate = GetCurrentStartDate(clock);

            int count = 0;
            while ((MaxOccurrences == null || count < MaxOccurrences) &&
                   (EndDate == null || currentDate <= EndDate) &&
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
            var now = clock.Now;
            // Если дата начала ещё не наступила
            if (now < StartDate)
                return StartDate;

            // Ищем последнюю прошедшую дату, чтобы начать с новой
            var lastOccurrence = FindLastOccurrence(now);
            return lastOccurrence.HasValue ? CalculateNextDate(lastOccurrence.Value) : StartDate;
        }

        private DateTime? FindLastOccurrence(DateTime before)
        {
            var result = StartDate;
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
            var daysSinceStart = (current.Date - StartDate.Date).Days;

            if (daysSinceStart < 0)
                return StartDate;

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
