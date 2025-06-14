
namespace PingMeTasks.Data.SqlServer.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Если null → "Без категории"
        public bool IsDefault { get; set; } // Категория по умолчанию

        // Внешний ключ
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        // Навигационное свойство
        public List<Task> Tasks { get; set; } = new();
    }
}
