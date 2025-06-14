using PingMeTasks.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Core.Domain
{
    /// <summary>
    /// Описание повторения задачи
    /// </summary>
    public class TaskRecurrence
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
            // Логика расчета следующей даты в зависимости от типа
            return Type switch
            {
                RepeatType.Daily => current.AddDays(Interval),
                RepeatType.Weekly when DayOfWeek.HasValue => current.AddDays(7 * Interval).Next(DayOfWeek.Value),
                RepeatType.Monthly when DayOfMonth.HasValue => current.AddMonths(Interval).SetDay(DayOfMonth.Value),
                RepeatType.Yearly when MonthOfYear.HasValue && DayOfMonth.HasValue =>
                    current.AddYears(Interval).SetMonth(MonthOfYear.Value).SetDay(DayOfMonth.Value),
                _ => current.AddDays(Interval) // Custom
            };
        }
    }

    public enum RepeatType { Daily, Weekly, Monthly, Yearly, Custom }

    //RecurrenceType
}
