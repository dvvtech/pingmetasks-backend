using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Data.SqlServer.Entities.Notifications
{
    public class PushUpSettingsEntity
    {
        public int Id { get; set; }
        public string DeviceToken { get; set; } // FCM-токен
        public bool IsEnabled { get; set; }

        // Внешний ключ
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
