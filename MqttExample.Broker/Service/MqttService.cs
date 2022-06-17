using MQTTnet.Server;

namespace MqttExample.Broker.Service
{
    public class MqttService : IMqttService
    {
        public IMqttServer Server { get; }
        public IList<string> ConnectedClientIds { get; } = new List<string>();

        public MqttService(/*IMqttServer mqttServer*/)
        {
            // Server = mqttServer;
        }
    }
}