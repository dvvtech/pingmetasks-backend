﻿
namespace PingMeTasks.Data.SqlServer.Entities.Notifications
{
    public class TelegramSettingsEntity
    {
        public int Id { get; set; }
        public long ChannelId { get; set; }
        public string? AccessHash { get; set; }
        public bool IsEnabled { get; set; }

        // Внешний ключ
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
