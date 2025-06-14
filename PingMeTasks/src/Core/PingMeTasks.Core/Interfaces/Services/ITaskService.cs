using PingMeTasks.Contracts.DTOs.Requests;
using PingMeTasks.Contracts.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingMeTasks.Core.Interfaces.Services
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request, int userId);
    }
}
