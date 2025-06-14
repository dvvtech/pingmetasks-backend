

namespace PingMeTasks.Core.Interfaces.Repositories
{
    public interface ITaskItemRepository
    {
        public Task AddAsync(PingMeTasks.Core.Domain.TaskItem task);

        public Task SaveChangesAsync();
    }
}
