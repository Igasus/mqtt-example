namespace MqttExample.Client.Exceptions
{
    public class MqttClientIsNotConnectedException : Exception
    {
        public MqttClientIsNotConnectedException(string message)
            : base(message)
        { }
    }
}