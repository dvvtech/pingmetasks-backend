namespace PingMeTasks.Data.SqlServer.Entities.Notifications
{
    public class NotificationEntity
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public DateTime ScheduledTime { get; set; }
        public bool IsSent { get; set; }

        // Внешние ключи
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }

    public enum NotificationType { Telegram, PushUp, Email }
}
