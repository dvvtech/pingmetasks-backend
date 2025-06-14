using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Core.Domain
{
    public class PushUpSettings
    {
        public int Id { get; set; }
        public string DeviceToken { get; set; } // FCM-токен
        public bool IsEnabled { get; set; }

        // Внешний ключ
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
