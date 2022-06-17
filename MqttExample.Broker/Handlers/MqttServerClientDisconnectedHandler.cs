using System.Globalization;
using MqttExample.Broker.Service;
using MQTTnet.Server;

namespace MqttExample.Broker.Handlers
{
    public class MqttServerClientDisconnectedHandler : IMqttServerClientDisconnectedHandler
    {
        private readonly IMqttService _mqttService;
        private readonly ILogger<MqttServerClientDisconnectedHandler> _logger;

        public MqttServerClientDisconnectedHandler(IMqttService mqttService,
            ILogger<MqttServerClientDisconnectedHandler> logger)
        {
            _mqttService = mqttService;
            _logger = logger;
        }
        
        public Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            var log = $"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} - Received MQTT Message Logged:\n" +
                "- HandleClientDisconnectedAsync Handler Triggered:\n" +
                $"  |- ClientId: {eventArgs.ClientId}\n" +
                $"  |- DisconnectType: {eventArgs.DisconnectType}\n";
            
            _logger.LogInformation(log);
            
            _mqttService.ConnectedClientIds.Remove(eventArgs.ClientId);
            
            return Task.CompletedTask;
        }
    }
}