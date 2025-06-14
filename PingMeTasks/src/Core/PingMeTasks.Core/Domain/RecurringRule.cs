using PingMeTasks.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private List<DateTime> GenerateOccurrences()
        {
            // Здесь реализуется логика генерации дат по правилам
            var occurrences = new List<DateTime>();
            var currentDate = StartDate;

            int count = 0;
            while ((MaxOccurrences == null || count < MaxOccurrences) &&
                   (EndDate == null || currentDate <= EndDate))
            {
                occurrences.Add(currentDate);
                currentDate = CalculateNextDate(currentDate);
                count++;
            }

            return occurrences;
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
