using System.Globalization;
using System.Text;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace MqttExample.Broker.Handlers
{
    public class MqttApplicationMessageReceivedHandler : IMqttApplicationMessageReceivedHandler
    {
        private readonly ILogger<MqttApplicationMessageReceivedHandler> _logger;

        public MqttApplicationMessageReceivedHandler(ILogger<MqttApplicationMessageReceivedHandler> logger)
        {
            _logger = logger;
        }
        
        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var log = $"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} - Received MQTT Message Logged:\n" + 
                "- InterceptApplicationMessagePublishAsync Handler Triggered:\n" +
                $"  |- ClientId: {eventArgs.ClientId}\n" +
                "  |- ApplicationMessage:\n" +
                $"     |- Topic: {eventArgs.ApplicationMessage.Topic}\n" +
                $"     |- Payload: {Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload)}\n" +
                $"     |- QoS: {eventArgs.ApplicationMessage.QualityOfServiceLevel}\n" +
                $"     |- Retain: {eventArgs.ApplicationMessage.Retain}\n";
            
            _logger.LogInformation(log);
            
            return Task.CompletedTask;
        }
    }
}