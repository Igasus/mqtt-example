using Microsoft.Extensions.Options;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace MqttExample.Broker.Helpers
{
    public class MqttServerConnectionValidator : IMqttServerConnectionValidator
    {
        private readonly MqttOptions _mqttOptions;

        public MqttServerConnectionValidator(IOptions<MqttOptions> mqttOptions)
        {
            _mqttOptions = mqttOptions.Value;
        }
        
        public Task ValidateConnectionAsync(MqttConnectionValidatorContext context)
        {
            if (context.Username != _mqttOptions.Username || context.Password != _mqttOptions.Password)
            {
                context.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                return Task.CompletedTask;
            }

            context.ReasonCode = MqttConnectReasonCode.Success;
            return Task.CompletedTask;
        }
    }
}