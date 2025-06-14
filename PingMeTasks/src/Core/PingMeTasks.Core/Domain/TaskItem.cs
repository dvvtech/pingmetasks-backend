using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Core.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;        

        // Внешние ключи
        public int UserId { get; set; }
        public User User { get; set; }

        public int? CategoryId { get; set; } // Nullable, если категория не задана
        public Category? Category { get; set; }

        // Навигационные свойства
        public TaskRecurrence? Recurrence { get; set; }
        public List<Notification> Notifications { get; set; } = new();
    }    
}
