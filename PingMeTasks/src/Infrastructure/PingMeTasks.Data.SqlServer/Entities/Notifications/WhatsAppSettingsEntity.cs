
namespace PingMeTasks.Data.SqlServer.Entities.Notifications
{
    public class WhatsAppSettingsEntity
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        // Внешний ключ
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
