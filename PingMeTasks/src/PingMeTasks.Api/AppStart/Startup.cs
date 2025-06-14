using PingMeTasks.Core.Interfaces.Repositories;
using PingMeTasks.Core.Interfaces.Services;
using PingMeTasks.Core.Services;
using PingMeTasks.Data.SqlServer.Repositories;

namespace PingMeTasks.Api.AppStart
{
    public partial class Startup
    {
        private WebApplicationBuilder _builder;

        public void Initialize(WebApplicationBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ITaskItemService, TaskItemService>();
            builder.Services.AddScoped<ITaskItemRepository, TaskRepository>();
        }
    }
}
