using MQTTnet.Extensions.ManagedClient;

namespace MqttExample.Client.EventHandlers
{
    public class MqttConnectingFailedHandler : IMqttConnectingFailedHandler
    {
        private readonly ILogger<MqttConnectingFailedHandler> _logger;

        public MqttConnectingFailedHandler(ILogger<MqttConnectingFailedHandler> logger)
        {
            _logger = logger;
        }

        public void OnConnectingFailed(ManagedProcessFailedEventArgs eventArgs)
        {
            _logger.LogWarning("Mqtt connection failed");
        }
    }
}