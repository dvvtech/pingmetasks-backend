using PingMeTasks.Contracts.DTOs.Requests;
using PingMeTasks.Contracts.DTOs.Responses;
using PingMeTasks.Core.Domain;
using PingMeTasks.Core.Interfaces.Repositories;
using PingMeTasks.Core.Interfaces.Services;

namespace PingMeTasks.Core.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskRepository;

        public TaskItemService(ITaskItemRepository taskRepository)
        {
            _taskRepository = taskRepository;
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
                task.Recurrence = new RecurringRule
                {
                    Type = (RepeatType)request.Recurrence.Type,
                    Interval = request.Recurrence.Interval,
                    /*DaysOfWeek = request.Recurrence.DaysOfWeek != null
                        ? string.Join(",", request.Recurrence.DaysOfWeek)
                        : null,*/
                    DayOfMonth = request.Recurrence.DayOfMonth,
                    EndDate = request.Recurrence.EndDate
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
                Recurrence = task.Recurrence != null ? new RecurrenceDto
                {
                    Type = (PingMeTasks.Contracts.DTOs.RecurrenceType)task.Recurrence.Type,
                    Interval = task.Recurrence.Interval,
                    //DaysOfWeek = task.Recurrence.DaysOfWeek?.Split(',').ToList()
                } : null
            };
        }
    }
}
