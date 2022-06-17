using System.Globalization;
using MQTTnet.Server;

namespace MqttExample.Broker.Handlers
{
    public class MqttServerStoppedHandler : IMqttServerStoppedHandler
    {
        private readonly ILogger<MqttServerStoppedHandler> _logger;

        public MqttServerStoppedHandler(ILogger<MqttServerStoppedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleServerStoppedAsync(EventArgs eventArgs)
        {
            var log = $"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} - Received MQTT Message Logged:\n" + 
                "- HandleServerStoppedAsync Handler Triggered\n";
            
            _logger.LogInformation(log);
            
            return Task.CompletedTask;
        }
    }
}