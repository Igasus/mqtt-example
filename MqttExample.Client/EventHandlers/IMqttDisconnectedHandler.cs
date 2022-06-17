using MQTTnet.Client.Disconnecting;

namespace MqttExample.Client.EventHandlers
{
    public interface IMqttDisconnectedHandler
    {
        void OnDisconnected(MqttClientDisconnectedEventArgs eventArgs);
    }
}