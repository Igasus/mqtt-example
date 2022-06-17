namespace MqttExample.Client.Manager
{
    public interface IMqttClientManager
    {
        Task SubscribeAsync(string topic, int qos = 1);
        Task PublishAsync(string topic, string payload, bool retainFlag = true, int qos = 1);
    }
}