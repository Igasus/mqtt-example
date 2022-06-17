using System.Globalization;
using MQTTnet.Server;

namespace MqttExample.Broker.Handlers
{
    public class MqttServerStartedHandler : IMqttServerStartedHandler
    {
        private readonly ILogger<MqttServerStartedHandler> _logger;

        public MqttServerStartedHandler(ILogger<MqttServerStartedHandler> logger)
        {
            _logger = logger;
        }
        
        public Task HandleServerStartedAsync(EventArgs eventArgs)
        {
            var log = $"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} - Received MQTT Message Logged:\n" + 
                "- HandleServerStartedAsync Handler Triggered\n";
            
            _logger.LogInformation(log);
            
            return Task.CompletedTask;
        }
    }
}