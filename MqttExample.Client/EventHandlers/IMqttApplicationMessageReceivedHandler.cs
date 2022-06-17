using MQTTnet;

namespace MqttExample.Client.EventHandlers
{
    public interface IMqttApplicationMessageReceivedHandler
    {
        void OnApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs);
    }
}