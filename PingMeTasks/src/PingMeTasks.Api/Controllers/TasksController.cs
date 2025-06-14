using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PingMeTasks.Contracts.DTOs.Requests;
using PingMeTasks.Contracts.DTOs.Responses;
using PingMeTasks.Core.Interfaces.Services;
using System.Security.Claims;

namespace PingMeTasks.Api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponse>> CreateTask([FromBody] CreateTaskRequest request)
        {
            // Получаем userId из JWT или другого контекста
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var taskDto = await _taskService.CreateTaskAsync(request, userId);
            return Ok(taskDto);
            //return CreatedAtAction(nameof(GetTask), new { id = taskDto.Id }, taskDto);
        }
    }
}
