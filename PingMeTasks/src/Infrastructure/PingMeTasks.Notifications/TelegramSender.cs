using PingMeTasks.Core.Domain;
using PingMeTasks.Core.Interfaces.Services;

namespace PingMeTasks.Notifications
{    
    public class TelegramSender : INotificationSender
    {
        public async System.Threading.Tasks.Task SendAsync(Notification notification)
        {
            // Использует Telegram.Bot для отправки

            await System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
