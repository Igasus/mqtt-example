using MqttExample.Broker.Configurator;
using MqttExample.Broker.Handlers;
using MqttExample.Broker.Helpers;
using MqttExample.Broker.Service;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.AttributeRouting;
using MQTTnet.AspNetCore.Extensions;
using MQTTnet.Client.Receiving;
using MQTTnet.Server;

namespace MqttExample.Broker
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
            services.AddMqttControllers();
            services.AddHostedMqttServerWithServices(mqttServerOptionsBuilder =>
                {
                    var mqttServerConfigurator = mqttServerOptionsBuilder.ServiceProvider
                        .GetRequiredService<IMqttServerConfigurator>();

                    mqttServerConfigurator.ConfigureMqttServerOptions(mqttServerOptionsBuilder);
                })
                .AddMqttConnectionHandler()
                .AddConnections()
                .AddMqttWebSocketServerAdapter();

            services.AddSingleton<IMqttService, MqttService>()
                .AddTransient<IMqttServerConfigurator, MqttServerConfigurator>()
                .AddTransient<IMqttApplicationMessageReceivedHandler, MqttApplicationMessageReceivedHandler>()
                .AddTransient<IMqttServerApplicationMessageInterceptor, MqttServerApplicationMessageInterceptor>()
                .AddTransient<IMqttServerClientConnectedHandler, MqttServerClientConnectedHandler>()
                .AddTransient<IMqttServerClientDisconnectedHandler, MqttServerClientDisconnectedHandler>()
                .AddTransient<IMqttServerClientSubscribedTopicHandler, MqttServerClientSubscribedTopicHandler>()
                .AddTransient<IMqttServerClientUnsubscribedTopicHandler, MqttServerClientUnsubscribedTopicHandler>()
                .AddTransient<IMqttServerConnectionValidator, MqttServerConnectionValidator>()
                .AddTransient<IMqttServerStartedHandler, MqttServerStartedHandler>()
                .AddTransient<IMqttServerStoppedHandler, MqttServerStoppedHandler>();

            services.Configure<MqttOptions>(_configuration.GetSection(MqttOptions.Section));
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IMqttServerConfigurator mqttServerConfigurator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapConnectionHandler<MqttConnectionHandler>("/mqtt",
                    httpConnectionDispatcherOptions =>
                        httpConnectionDispatcherOptions.WebSockets.SubProtocolSelector = protocolList =>
                            protocolList.FirstOrDefault() ?? string.Empty);
            });

            app.UseMqttServer(mqttServerConfigurator.ConfigureMqttServer);
        }
    }
}