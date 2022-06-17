using MqttExample.Client.Exceptions;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;

namespace MqttExample.Client.Manager
{
    public class MqttClientManager : IMqttClientManager
    {
        private readonly IManagedMqttClient _mqttClient;

        private const string MqttIsNotConnectedSubscribeMessage =
            "Unable to Subscribe while Mqtt-Client is not connected.";
        private const string MqttIsNotConnectedPublishMessage ="Unable to Publish while Mqtt-Client is not connected.";

        public MqttClientManager(IManagedMqttClient mqttClient)
        {
            _mqttClient = mqttClient;
        }

        public async Task SubscribeAsync(string topic, int qos = 1)
        {
            if (!_mqttClient.IsConnected)
            {
                throw new MqttClientIsNotConnectedException(MqttIsNotConnectedSubscribeMessage);
            }
            
            var topicFilterBuilder = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel) qos);
            
            await _mqttClient.SubscribeAsync(topicFilterBuilder.Build());
        }

        public async Task PublishAsync(string topic, string payload, bool retainFlag = true, int qos = 1)
        {
            if (!_mqttClient.IsConnected)
            {
                throw new MqttClientIsNotConnectedException(MqttIsNotConnectedPublishMessage);
            }

            var messageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel) qos)
                .WithRetainFlag(retainFlag);
            
            await _mqttClient.PublishAsync(messageBuilder.Build());
        }
    }
}