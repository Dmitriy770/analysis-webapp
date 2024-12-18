using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using StudyServices.Infrastructure.Settings;

namespace StudyServices.Infrastructure.Common.Kafka;

internal sealed class KafkaClientHandle : IDisposable
{
    public KafkaClientHandle(StudyProducerSettings settings)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = settings.Servers,
            SecurityProtocol = SecurityProtocol.Plaintext,
            SaslMechanism = SaslMechanism.ScramSha512,
            SaslUsername = settings.Username,
            SaslPassword = settings.Password
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