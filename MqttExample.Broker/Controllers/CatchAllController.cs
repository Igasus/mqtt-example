using MQTTnet.AspNetCore.AttributeRouting;

namespace MqttExample.Broker.Controllers
{
    [MqttController]
    public class CatchAllController : MqttBaseController
    {
        private readonly ILogger<CatchAllController> _logger;

        public CatchAllController(ILogger<CatchAllController> logger)
        {
            _logger = logger;
        }

        [MqttRoute("{*topic}")]
        public Task LogPublishedTopicAsync(string topic)
        {
            _logger.LogInformation($"Catch All Controller - Published Topic: {topic}");

            return Ok();
        }
    }
}