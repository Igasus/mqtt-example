using System.Globalization;
using MqttExample.Broker.Service;
using MQTTnet.Server;

namespace MqttExample.Broker.Handlers
{
    public class MqttServerClientConnectedHandler : IMqttServerClientConnectedHandler
    {
        private readonly IMqttService _mqttService;
        private readonly ILogger<MqttServerClientConnectedHandler> _logger;

        public MqttServerClientConnectedHandler(IMqttService mqttService,
            ILogger<MqttServerClientConnectedHandler> logger)
        {
            _mqttService = mqttService;
            _logger = logger;
        }
        
        public Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            var log = $"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} - Received MQTT Message Logged:\n" +
                "- HandleClientConnectedAsync Handler Triggered:\n" +
                $"  |- ClientId: {eventArgs.ClientId}\n";
            
            _logger.LogInformation(log);
            
            _mqttService.ConnectedClientIds.Add(eventArgs.ClientId);
            
            return Task.CompletedTask;
        }
    }
}