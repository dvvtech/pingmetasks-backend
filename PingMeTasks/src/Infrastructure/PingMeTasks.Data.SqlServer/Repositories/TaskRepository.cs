using PingMeTasks.Core.Interfaces.Repositories;
using PingMeTasks.Data.SqlServer.Context;
using PingMeTasks.Data.SqlServer.Entities;

namespace PingMeTasks.Data.SqlServer.Repositories
{
    public class TaskRepository : ITaskItemRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PingMeTasks.Core.Domain.TaskItem task)
        {
            //мапим его в Entity

            await _context.Tasks.AddAsync(task);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
