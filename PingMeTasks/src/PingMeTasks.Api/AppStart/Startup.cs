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
        }
    }
}
