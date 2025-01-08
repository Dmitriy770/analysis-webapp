using Confluent.Kafka;
using StudyServices.Infrastructure.Settings;

namespace StudyServices.Infrastructure.Common.Kafka;

internal sealed class KafkaClientHandle : IDisposable
{
    public KafkaClientHandle(StudyProducerSettings settings)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = settings.Servers,
            SecurityProtocol = SecurityProtocol.SaslPlaintext,
            SaslMechanism = SaslMechanism.Plain,
            SaslUsername = settings.Username,
            SaslPassword = settings.Password,
            AllowAutoCreateTopics = true,
            ClientId = "StudyServices"
        };
            
        _kafkaProducer = new ProducerBuilder<byte[], byte[]>(producerConfig).Build();
    }
    
    public void Dispose()
    {
        _kafkaProducer.Flush();
        _kafkaProducer.Dispose();
    }
    
    public Handle Handle => _kafkaProducer.Handle;
    private readonly IProducer<byte[], byte[]> _kafkaProducer;
}