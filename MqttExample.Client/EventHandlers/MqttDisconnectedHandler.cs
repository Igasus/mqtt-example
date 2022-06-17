using MQTTnet.Client.Disconnecting;

namespace MqttExample.Client.EventHandlers
{
    public class MqttDisconnectedHandler : IMqttDisconnectedHandler
    {
        private readonly ILogger<MqttDisconnectedHandler> _logger;

        public MqttDisconnectedHandler(ILogger<MqttDisconnectedHandler> logger)
        {
            _logger = logger;
        }

        public void OnDisconnected(MqttClientDisconnectedEventArgs eventArgs)
        {
            _logger.LogInformation("Mqtt has been disconnected");
        }
    }
}