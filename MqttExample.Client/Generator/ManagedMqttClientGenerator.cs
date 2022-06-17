using Microsoft.Extensions.Options;
using MqttExample.Client.EventHandlers;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using IMqttApplicationMessageReceivedHandler = MqttExample.Client.EventHandlers.IMqttApplicationMessageReceivedHandler;

namespace MqttExample.Client.Generator
{
    public class ManagedMqttClientGenerator : IManagedMqttClientGenerator
    {
        private readonly IMqttApplicationMessageReceivedHandler _applicationMessageReceivedHandler;
        private readonly IMqttConnectedHandler _connectedHandler;
        private readonly IMqttConnectingFailedHandler _connectingFailedHandler;
        private readonly IMqttDisconnectedHandler _disconnectedHandler;
        private readonly MqttOptions _mqttOptions;

        public ManagedMqttClientGenerator(IMqttApplicationMessageReceivedHandler mqttApplicationMessageReceivedHandler,
            IMqttConnectedHandler mqttConnectedHandler,
            IMqttConnectingFailedHandler mqttConnectingFailedHandler,
            IMqttDisconnectedHandler mqttDisconnectedHandler,
            IOptions<MqttOptions> mqttOptions)
        {
            _applicationMessageReceivedHandler = mqttApplicationMessageReceivedHandler;
            _connectedHandler = mqttConnectedHandler;
            _connectingFailedHandler = mqttConnectingFailedHandler;
            _disconnectedHandler = mqttDisconnectedHandler;
            _mqttOptions = mqttOptions.Value;
        }
        
        public async Task<IManagedMqttClient> GenerateManagedMqttClientAsync()
        {
            var messageBuilder = new MqttClientOptionsBuilder()
                .WithClientId(_mqttOptions.ClientId)
                .WithCredentials(_mqttOptions.ClientUsername, _mqttOptions.ClientPassword)
                .WithTcpServer(_mqttOptions.BrokerUri, _mqttOptions.BrokerPort)
                .WithCleanSession();

            var options = _mqttOptions.MqttSecure
                ? messageBuilder
                    .WithTls()
                    .Build()
                : messageBuilder
                    .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(_mqttOptions.AutoReconnectDelaySeconds))
                .WithClientOptions(options)
                .Build();

            var mqttClient = new MqttFactory().CreateManagedMqttClient();

            AddMqttEventHandlers(mqttClient);

            await mqttClient.StartAsync(managedOptions);

            return mqttClient;
        }

        public void AddMqttEventHandlers(IManagedMqttClient mqttClient)
        {
            mqttClient.ApplicationMessageReceivedHandler =
                new MqttApplicationMessageReceivedHandlerDelegate(_applicationMessageReceivedHandler
                    .OnApplicationMessageReceived);
            
            mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(_connectedHandler.OnConnected);
            
            mqttClient.ConnectingFailedHandler =
                new ConnectingFailedHandlerDelegate(_connectingFailedHandler.OnConnectingFailed);
            
            mqttClient.DisconnectedHandler =
                new MqttClientDisconnectedHandlerDelegate(_disconnectedHandler.OnDisconnected);
        }
    }
}