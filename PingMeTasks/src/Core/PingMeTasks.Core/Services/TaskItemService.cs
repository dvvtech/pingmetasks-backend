using PingMeTasks.Contracts.DTOs.Requests;
using PingMeTasks.Contracts.DTOs.Responses;
using PingMeTasks.Core.Domain;
using PingMeTasks.Core.Interfaces.Repositories;
using PingMeTasks.Core.Interfaces.Services;
using System;

namespace PingMeTasks.Core.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskRepository;

        private readonly AppDbContext _db;

        public TaskItemService(ITaskItemRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskItem>> GetDueTasksAsync(DateTime nowUtc, CancellationToken ct)
        {
            return await _db.Tasks
                .Include(t => t.RecurringRule)
                .Where(t => t.DueDateUtc <= nowUtc && !t.IsCompleted)
                .ToListAsync(ct);
        }

        public async Task ProcessRecurringTaskAsync(TaskItem task, CancellationToken ct)
        {
            var rule = task.RecurringRule;

            // Вычисляем следующую дату
            var nextDate = rule.GetNextOccurrence(task.DueDateUtc, nowUtc);

            if (rule.EndDateUtc.HasValue && nextDate > rule.EndDateUtc)
            {
                task.IsCompleted = true;
            }
            else
            {
                task.DueDateUtc = nextDate;
            }

            await _db.SaveChangesAsync(ct);
        }

        public async Task MarkAsCompletedAsync(int taskId, CancellationToken ct)
        {
            var task = await _db.Tasks.FindAsync(new object[] { taskId }, ct);
            if (task != null)
            {
                task.IsCompleted = true;
                await _db.SaveChangesAsync(ct);
            }
        }

        public async Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request, int userId)
        {
            // Маппинг CreateTaskRequest → Task
            var task = new PingMeTasks.Core.Domain.TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                StartDate = request.DueDate,                
                UserId = userId,
                CategoryId = request.CategoryId
            };

            // Маппинг рекуррентности (если есть)
            if (request.Recurrence != null)
            {
                task.RecurringRule = new RecurringRule
                {
                    Type = (RepeatType)request.Recurrence.Type,
                    Interval = request.Recurrence.Interval,
                    /*DaysOfWeek = request.Recurrence.DaysOfWeek != null
                        ? string.Join(",", request.Recurrence.DaysOfWeek)
                        : null,*/
                    DayOfMonth = request.Recurrence.DayOfMonth,
                    EndDateUtc = request.Recurrence.EndDate
                };
            }

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            // Маппинг Task → TaskDto (для ответа API)
            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.StartDate,                
                Recurrence = task.RecurringRule != null ? new RecurrenceDto
                {
                    Type = (PingMeTasks.Contracts.DTOs.RecurrenceType)task.RecurringRule.Type,
                    Interval = task.RecurringRule.Interval,
                    //DaysOfWeek = task.Recurrence.DaysOfWeek?.Split(',').ToList()
                } : null
            };
        }
    }
}
