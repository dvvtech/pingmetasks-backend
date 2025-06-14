using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Core.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Если null → "Без категории"
        public bool IsDefault { get; set; } // Категория по умолчанию

        // Внешний ключ
        public int UserId { get; set; }
        public User User { get; set; }

        // Навигационное свойство
        public List<TaskItem> TaskItems { get; set; } = new();        
    }
}
