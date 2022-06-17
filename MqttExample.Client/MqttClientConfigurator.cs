using MqttExample.Client.EventHandlers;
using MqttExample.Client.Generator;
using MqttExample.Client.Manager;

namespace MqttExample.Client
{
    public static class MqttClientConfigurator
    {
        public static void ConfigureMqttClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MqttOptions>(configuration.GetSection(MqttOptions.Section));

            services.AddTransient<IMqttApplicationMessageReceivedHandler, MqttApplicationMessageReceivedHandler>()
                .AddTransient<IMqttConnectedHandler, MqttConnectedHandler>()
                .AddTransient<IMqttConnectingFailedHandler, MqttConnectingFailedHandler>()
                .AddTransient<IMqttDisconnectedHandler, MqttDisconnectedHandler>()
                .AddTransient<IManagedMqttClientGenerator, ManagedMqttClientGenerator>()
                .AddTransient<IMqttClientManager, MqttClientManager>();

            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IManagedMqttClientGenerator>()
                    .GenerateManagedMqttClientAsync().GetAwaiter().GetResult());
        }
    }
}