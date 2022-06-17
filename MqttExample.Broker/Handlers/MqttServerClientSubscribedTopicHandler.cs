using System.Globalization;
using MQTTnet.Server;

namespace MqttExample.Broker.Handlers
{
    public class MqttServerClientSubscribedTopicHandler : IMqttServerClientSubscribedTopicHandler
    {
        private readonly ILogger<MqttServerClientSubscribedTopicHandler> _logger;

        public MqttServerClientSubscribedTopicHandler(ILogger<MqttServerClientSubscribedTopicHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleClientSubscribedTopicAsync(MqttServerClientSubscribedTopicEventArgs eventArgs)
        {
            var log = $"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} - Received MQTT Message Logged:\n" +
                "- HandleClientSubscribedTopicAsync Handler Triggered:\n" +
                $"  |- ClientId: {eventArgs.ClientId}\n" +
                "  |- TopicFilter:\n" +
                $"     |- Topic: {eventArgs.TopicFilter.Topic}\n" +
                $"     |- QoS: {eventArgs.TopicFilter.QualityOfServiceLevel}\n";
            
            _logger.LogInformation(log);

            return Task.CompletedTask;
        }
    }
}