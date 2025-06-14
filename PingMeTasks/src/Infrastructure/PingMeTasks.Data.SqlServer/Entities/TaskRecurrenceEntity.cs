
namespace PingMeTasks.Data.SqlServer.Entities
{
    /// <summary>
    /// Повторение задачи
    /// </summary>
    public class TaskRecurrenceEntity
    {
        public int Id { get; set; }
        public RecurrenceType Type { get; set; }
        public int Interval { get; set; } = 1; // Каждые N дней/недель/месяцев
        public string? DaysOfWeek { get; set; } // "Monday,Wednesday" (для Weekly)
        public int? DayOfMonth { get; set; } // Для Monthly
        public DateTime? EndDate { get; set; } // Окончание повторений
        //public string? CustomCronExpression { get; set; } // Для сложных сценариев

        // Внешний ключ
        public int TaskItemId { get; set; }
        public TaskItemEntity TaskItem { get; set; }
    }

    public enum RecurrenceType { Daily, Weekly, Monthly, Yearly, Custom }
}
