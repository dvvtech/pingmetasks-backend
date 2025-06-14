using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Core.Domain
{
    public class Notification
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public DateTime ScheduledTime { get; set; }
        public bool IsSent { get; set; }

        // Внешние ключи
        public int UserId { get; set; }
        public User User { get; set; }

        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }
    }

    public enum NotificationType { Telegram, PushUp, Email }
}
