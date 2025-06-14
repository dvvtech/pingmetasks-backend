using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Data.SqlServer.Context
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Если null → "Без категории"
        public bool IsDefault { get; set; } // Категория по умолчанию

        // Внешний ключ
        public int UserId { get; set; }
        public User User { get; set; }

        // Навигационное свойство
        public List<Task> Tasks { get; set; } = new();
    }
}
