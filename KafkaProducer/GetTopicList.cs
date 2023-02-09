using Confluent.Kafka;

namespace KafkaProducer
{
    public class GetTopicList
    {
        private const string BootstrapServer = "localhgost:9092";
        private readonly AdminClientConfig _confeig;
        public GetTopicList()
        {
            _confeig = new AdminClientConfig()
            {
                BootstrapServers = BootstrapServer
            };
        }

        private List<string> Get()
        {
            var adminClient = new AdminClientBuilder(_confeig).Build();
            var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
            var topicsMetadata = metadata.Topics;
            var topicsName = metadata.Topics.Select(a => a.Topic).ToList();
            return topicsName;

        }
    }
}
