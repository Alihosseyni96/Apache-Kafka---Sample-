using Confluent.Kafka.Admin;
using Confluent.Kafka;

namespace KafkaProducer
{
    public class CreateTopic
    {
        private  const string bootstrapServers = "localhost:9092";
        public void createTopic(string topicName)
        {
            //create Topic if you need
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build())
            {
                 adminClient.CreateTopicsAsync(new TopicSpecification[]
                {
                new TopicSpecification{Name = topicName , ReplicationFactor
                 = 1 , NumPartitions = 1}
                });
            }
            //

        }
    }
}
