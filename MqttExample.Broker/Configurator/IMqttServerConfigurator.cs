using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace MqttExample.Broker.Configurator
{
    public interface IMqttServerConfigurator
    {
        void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options);
        void ConfigureMqttServer(IMqttServer mqtt);
    }
}