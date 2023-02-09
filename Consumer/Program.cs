// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;

var config = new ConsumerConfig()
{
    GroupId = "Consumer",
    BootstrapServers = "localhost:9092"
};

using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
{
    consumer.Subscribe("jerry");
    while (true)
    {

        var cr = consumer.Consume();
        Console.WriteLine(cr.Message.Value);
    }
}