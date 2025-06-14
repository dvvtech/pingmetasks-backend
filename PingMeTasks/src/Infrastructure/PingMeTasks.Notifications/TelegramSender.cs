using PingMeTasks.Core.Domain;
using PingMeTasks.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Notifications
{    
    public class TelegramSender : INotificationSender
    {
        public async System.Threading.Tasks.Task SendAsync(Notification notification)
        {
            // Использует Telegram.Bot для отправки

            return System.Threading.Tasks.Task.FromResult();
        }
    }
}
