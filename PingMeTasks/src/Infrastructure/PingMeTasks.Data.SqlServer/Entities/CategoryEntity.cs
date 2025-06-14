
namespace PingMeTasks.Data.SqlServer.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// // Если null → "Без категории"
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Категория по умолчанию
        /// </summary>
        public bool IsDefault { get; set; }


        // Внешний ключ
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        // Навигационное свойство
        public List<TaskEntity> Tasks { get; set; } = new();
    }
}
