using Confluent.Kafka;
using Consumer_Api.Model;
using System.Diagnostics;
using System.Text.Json;

namespace Consumer_Api
{
    public class KafkaConsumer:IHostedService
    {
        ConsumerConfig config = new ConsumerConfig()
        {
            GroupId = "Consumer",
            BootstrapServers = "localhost:9092"
        };


        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var consumer = new Confluent.Kafka.ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("jerry");
                while (true)
                {
                    var cr = consumer.Consume();
                    var test = cr.Message.Value.ToString();
                    var MyModel = JsonSerializer.Deserialize<Order>(test);
                    Debug.WriteLine(MyModel.Status);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
