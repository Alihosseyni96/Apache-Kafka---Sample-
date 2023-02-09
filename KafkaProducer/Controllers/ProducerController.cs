using Confluent.Kafka;
using Confluent.Kafka.Admin;
using KafkaProducer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace KafkaProducer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly string
        bootstrapServers = "localhost:9092";
        private readonly string topic = "jerry";

        [HttpPost]
        public async Task<IActionResult>
        Post([FromBody] OrderRequest orderRequest)
        {
            string message = JsonSerializer.Serialize(orderRequest);
            return Ok(await SendOrderRequest(topic, message));
        }
        private async Task<bool> SendOrderRequest
        (string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            //create Topic if you need
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build())
            {
                await adminClient.CreateTopicsAsync(new TopicSpecification[]
                {
                new TopicSpecification{Name = "MyTopicName" , ReplicationFactor
                 = 1 , NumPartitions = 1}
                });
            }
            //

            

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(config).Build())
                {
                    
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"delivery Time Span {result.Timestamp.UtcDateTime}");
                return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
