namespace MqttExample.Client
{
    public class MqttOptions
    {
        public const string Section = "Mqtt";
        
        public string ClientId { get; set; }
        public string ClientUsername { get; set; }
        public string ClientPassword { get; set; }
        public string BrokerUri { get; set; }
        public int BrokerPort { get; set; }
        public bool MqttSecure { get; set; }
        public int AutoReconnectDelaySeconds { get; set; }
    }
}