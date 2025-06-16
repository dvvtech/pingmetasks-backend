using PingMeTasks.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Worker
{
    public class TaskSchedulerWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TaskSchedulerWorker> _logger;

        public TaskSchedulerWorker(
            IServiceProvider serviceProvider,
            ILogger<TaskSchedulerWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TaskSchedulerWorker is running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("TaskSchedulerWorker is checking tasks...");

                using var scope = _serviceProvider.CreateScope();
                var taskService = scope.ServiceProvider.GetRequiredService<ITaskItemService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                await ProcessDueTasksAsync(taskService, notificationService, stoppingToken);

                // Ждём 1 минуту перед следующей проверкой
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ProcessDueTasksAsync(
            ITaskItemService taskItemService,
            INotificationService notificationService,
            CancellationToken ct)
        {
            var nowUtc = DateTime.UtcNow;

            // Получаем все активные задачи, которые должны быть выполнены сейчас или были просрочены
            var dueTasks = await taskItemService.GetDueTasksAsync(nowUtc, ct);

            foreach (var task in dueTasks)
            {
                try
                {
                    // Уведомляем пользователя
                    await notificationService.SendNotificationAsync(task, ct);

                    // Обновляем или создаём новую задачу на следующее выполнение
                    if (task.RecurringRule != null)
                    {
                        await taskItemService.ProcessRecurringTaskAsync(task, ct);
                    }
                    else
                    {
                        // Одноразовая задача — завершаем
                        await taskItemService.MarkAsCompletedAsync(task.Id, ct);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing task {TaskId}", task.Id);
                }
            }
        }
    }
}
