using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Core.Domain
{
    public class TelegramSettings
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string? AccessHash { get; set; }
        public bool IsEnabled { get; set; }

        // Внешний ключ
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
