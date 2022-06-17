using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.AttributeRouting;
using MQTTnet.Client.Receiving;
using MQTTnet.Server;

namespace MqttExample.Broker.Configurator
{
    public class MqttServerConfigurator : IMqttServerConfigurator
    {
        private readonly IMqttApplicationMessageReceivedHandler _mqttApplicationMessageReceivedHandler;
        private readonly IMqttServerApplicationMessageInterceptor _mqttServerApplicationMessageInterceptor;
        private readonly IMqttServerClientConnectedHandler _mqttServerClientConnectedHandler;
        private readonly IMqttServerClientDisconnectedHandler _mqttServerClientDisconnectedHandler;
        private readonly IMqttServerClientSubscribedTopicHandler _mqttServerClientSubscribedTopicHandler;
        private readonly IMqttServerClientUnsubscribedTopicHandler _mqttServerClientUnsubscribedTopicHandler;
        private readonly IMqttServerConnectionValidator _mqttServerConnectionValidator;
        private readonly IMqttServerStartedHandler _mqttServerStartedHandler;
        private readonly IMqttServerStoppedHandler _mqttServerStoppedHandler;

        public MqttServerConfigurator(IMqttApplicationMessageReceivedHandler mqttApplicationMessageReceivedHandler,
            IMqttServerApplicationMessageInterceptor mqttServerApplicationMessageInterceptor,
            IMqttServerClientConnectedHandler mqttServerClientConnectedHandler,
            IMqttServerClientDisconnectedHandler mqttServerClientDisconnectedHandler,
            IMqttServerClientSubscribedTopicHandler mqttServerClientSubscribedTopicHandler,
            IMqttServerClientUnsubscribedTopicHandler mqttServerClientUnsubscribedTopicHandler,
            IMqttServerConnectionValidator mqttServerConnectionValidator,
            IMqttServerStartedHandler mqttServerStartedHandler,
            IMqttServerStoppedHandler mqttServerStoppedHandler)
        {
            _mqttServerConnectionValidator = mqttServerConnectionValidator;
            _mqttApplicationMessageReceivedHandler = mqttApplicationMessageReceivedHandler;
            _mqttServerApplicationMessageInterceptor = mqttServerApplicationMessageInterceptor;
            _mqttServerStartedHandler = mqttServerStartedHandler;
            _mqttServerStoppedHandler = mqttServerStoppedHandler;
            _mqttServerClientConnectedHandler = mqttServerClientConnectedHandler;
            _mqttServerClientDisconnectedHandler = mqttServerClientDisconnectedHandler;
            _mqttServerClientSubscribedTopicHandler = mqttServerClientSubscribedTopicHandler;
            _mqttServerClientUnsubscribedTopicHandler = mqttServerClientUnsubscribedTopicHandler;
        }
        
        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            // Configure the MQTT Server options here
            options.WithoutDefaultEndpoint();
            options.WithConnectionValidator(_mqttServerConnectionValidator);
            options.WithApplicationMessageInterceptor(_mqttServerApplicationMessageInterceptor);
            
            // Enable Attribute Routing
            // By default, messages published to topics that don't match any routes are rejected. 
            // Change this to true to allow those messages to be routed without hitting any controller actions.
            options.WithAttributeRouting(true);
        }

        public void ConfigureMqttServer(IMqttServer mqtt)
        {
            mqtt.ApplicationMessageReceivedHandler = _mqttApplicationMessageReceivedHandler;
            mqtt.StartedHandler = _mqttServerStartedHandler;
            mqtt.StoppedHandler = _mqttServerStoppedHandler;
            mqtt.ClientConnectedHandler = _mqttServerClientConnectedHandler;
            mqtt.ClientDisconnectedHandler = _mqttServerClientDisconnectedHandler;
            mqtt.ClientSubscribedTopicHandler = _mqttServerClientSubscribedTopicHandler;
            mqtt.ClientUnsubscribedTopicHandler = _mqttServerClientUnsubscribedTopicHandler;
        }
    }
}