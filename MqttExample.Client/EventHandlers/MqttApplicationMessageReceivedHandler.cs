using System.Text;
using MQTTnet;

namespace MqttExample.Client.EventHandlers
{
    public class MqttApplicationMessageReceivedHandler : IMqttApplicationMessageReceivedHandler
    {
        private ILogger<MqttApplicationMessageReceivedHandler> _logger;

        public MqttApplicationMessageReceivedHandler(ILogger<MqttApplicationMessageReceivedHandler> logger)
        {
            _logger = logger;
        }

        public void OnApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var topic = eventArgs.ApplicationMessage.Topic;
            var payload = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
            
            _logger.LogInformation($"Mqtt message received:\n|- Topic: {topic}\n|-Payload: {payload}");
        }
    }
}