﻿using PingMeTasks.Data.SqlServer.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Data.SqlServer.Entities
{
    public class TaskItemEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime CreatedUtcDate { get; set; }

        public DateTime UpdateUtcDate { get; set; }


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
