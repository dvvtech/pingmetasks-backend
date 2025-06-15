
namespace PingMeTasks.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Временная зона пользователя
        /// </summary>
        public string TimeZoneId { get; set; } = "UTC";

        // Навигационные свойства
        public List<Category> Categories { get; set; } = new();
        public List<TaskItem> TaskItems { get; set; } = new();

        //public TelegramSettings? TelegramSettings { get; set; }
        //public PushUpSettings? PushUpSettings { get; set; }
    }
}
