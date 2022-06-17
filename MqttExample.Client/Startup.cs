using MqttExample.Client.Manager;

namespace MqttExample.Client
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMqttClient(_configuration);
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMqttClientManager manager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Mqtt-Client App.");
                });
            });

            Task.Run(async () =>
            {
                await Task.Delay(5000);
                await manager.SubscribeAsync("test");
                await manager.PublishAsync("wow", "heh");
            });
        }
    }
}