using MQTTnet.Client.Connecting;

namespace MqttExample.Client.EventHandlers
{
    public interface IMqttConnectedHandler
    {
        void OnConnected(MqttClientConnectedEventArgs eventArgs);
    }
}