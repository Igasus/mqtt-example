using System.Globalization;
using MQTTnet.Server;

namespace MqttExample.Broker.Handlers
{
    public class MqttServerClientUnsubscribedTopicHandler : IMqttServerClientUnsubscribedTopicHandler
    {
        private readonly ILogger<MqttServerClientUnsubscribedTopicHandler> _logger;

        public MqttServerClientUnsubscribedTopicHandler(ILogger<MqttServerClientUnsubscribedTopicHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleClientUnsubscribedTopicAsync(MqttServerClientUnsubscribedTopicEventArgs eventArgs)
        {
            var log = $"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} - Received MQTT Message Logged:\n" +
                "- HandleClientUnsubscribedTopicAsync Handler Triggered:\n" +
                $"  |- ClientId: {eventArgs.ClientId}\n" +
                "  |- TopicFilter: {eventArgs.TopicFilter}\n";
            
            _logger.LogInformation(log);

            return Task.CompletedTask;
        }
    }
}