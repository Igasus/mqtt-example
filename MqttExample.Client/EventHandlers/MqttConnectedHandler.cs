using MQTTnet.Client.Connecting;

namespace MqttExample.Client.EventHandlers
{
    public class MqttConnectedHandler : IMqttConnectedHandler
    {
        private readonly ILogger<MqttConnectedHandler> _logger;

        public MqttConnectedHandler(ILogger<MqttConnectedHandler> logger)
        {
            _logger = logger;
        }

        public void OnConnected(MqttClientConnectedEventArgs eventArgs)
        {
            _logger.LogInformation("Mqtt has been connected");
        }
    }
}