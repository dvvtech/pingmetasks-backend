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
        public RecurrenceType Type { get; set; }
        public int Interval { get; set; } = 1; // Каждые N дней/недель/месяцев
        public string? DaysOfWeek { get; set; } // "Monday,Wednesday" (для Weekly)
        public int? DayOfMonth { get; set; } // Для Monthly
        public DateTime? StartDate { get; set; } // Начало повторений
        public DateTime? EndDate { get; set; } // Окончание повторений
        //public string? CustomCronExpression { get; set; } // Для сложных сценариев

        // Внешний ключ
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }
    }

    public enum RecurrenceType { Daily, Weekly, Monthly, Yearly, Custom }
}
