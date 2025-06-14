

namespace PingMeTasks.Core.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        public Task AddAsync(PingMeTasks.Core.Domain.Task task);

        public Task SaveChangesAsync();
    }
}
