using System.Text.Json;
using Confluent.Kafka;
using StudyService.Application.Abstractions.Producers;
using StudyService.Domain.Models.Studies;

namespace StudyServices.Infrastructure.Producers.Studies;

public class StudyProducer : IStudyProducer
{
    public async Task ProduceAsync(Study study, CancellationToken cancellationToken = default)
    {
        using var producer = CreateProducer();

        var topic = "topic";
        var message = JsonSerializer.Serialize(study);
        var deliveryReport = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message }, cancellationToken);
    }

    private IProducer<Null,string> CreateProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            ClientId = "study-producer",
        };
        
        return new ProducerBuilder<Null, string>(config).Build();
    }
}