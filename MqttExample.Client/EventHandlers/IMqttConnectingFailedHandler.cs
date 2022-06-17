using MQTTnet.Extensions.ManagedClient;

namespace MqttExample.Client.EventHandlers
{
    public interface IMqttConnectingFailedHandler
    {
        void OnConnectingFailed(ManagedProcessFailedEventArgs eventArgs);
    }
}