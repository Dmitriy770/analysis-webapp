using Confluent.Kafka;

namespace StudyServices.Infrastructure.Common.Kafka;

internal sealed class KafkaDependentProducer<TKey, TValue>(
    KafkaClientHandle handle)
{
    public Task ProduceAsync(string topic, Message<TKey, TValue> message, CancellationToken cancellationToken = default)
        => _kafkaHandle.ProduceAsync(topic, message, cancellationToken);
    
    public void Flush(TimeSpan timeout)
        => _kafkaHandle.Flush(timeout);
    
    private readonly IProducer<TKey, TValue> _kafkaHandle = new DependentProducerBuilder<TKey, TValue>(handle.Handle).Build();
}