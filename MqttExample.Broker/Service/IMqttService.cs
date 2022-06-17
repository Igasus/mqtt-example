using MQTTnet.Server;

namespace MqttExample.Broker.Service
{
    public interface IMqttService
    {
        public IMqttServer Server { get;}
        public IList<string> ConnectedClientIds { get; }
    }
}