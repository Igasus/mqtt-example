using System.Globalization;
using System.Text;
using MQTTnet.Server;

namespace MqttExample.Broker.Helpers
{
    public class MqttServerApplicationMessageInterceptor : IMqttServerApplicationMessageInterceptor
    {
        private readonly ILogger<MqttServerApplicationMessageInterceptor> _logger;

        public MqttServerApplicationMessageInterceptor(ILogger<MqttServerApplicationMessageInterceptor> logger)
        {
            _logger = logger;
        }

        public Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            var log = $"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} - Received MQTT Message Logged:\n" +
                "- InterceptApplicationMessagePublishAsync Handler Triggered:\n" +
                $"  |- ClientId: {context.ClientId}\n" +
                "  |- ApplicationMessage:\n" +
                $"     |- Topic: {context.ApplicationMessage.Topic}\n" +
                $"     |- Payload: {Encoding.UTF8.GetString(context.ApplicationMessage.Payload)}\n" +
                $"     |- QoS: {context.ApplicationMessage.QualityOfServiceLevel}\n" +
                $"     |- Retain: {context.ApplicationMessage.Retain}\n";

            _logger.LogInformation(log);

            return Task.CompletedTask;
        }
    }
}