using MQTTnet.Extensions.ManagedClient;

namespace MqttExample.Client.Generator
{
    public interface IManagedMqttClientGenerator
    {
        Task<IManagedMqttClient> GenerateManagedMqttClientAsync();
    }
}