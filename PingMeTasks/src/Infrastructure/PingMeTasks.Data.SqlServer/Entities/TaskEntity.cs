using PingMeTasks.Data.SqlServer.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Data.SqlServer.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Внешние ключи
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int? CategoryId { get; set; } // Nullable, если категория не задана
        public CategoryEntity? Category { get; set; }

        // Навигационные свойства
        public TaskRecurrenceEntity? Recurrence { get; set; }
        public List<NotificationEntity> Notifications { get; set; } = new();
    }
}
