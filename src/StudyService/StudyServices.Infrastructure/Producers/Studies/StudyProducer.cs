using System.Text.Json;
using Confluent.Kafka;
using StudyService.Application.Abstractions.Producers;
using StudyService.Domain.Models.Studies;
using StudyServices.Infrastructure.Common.Kafka;
using StudyServices.Infrastructure.Producers.Studies.Mappers;

namespace StudyServices.Infrastructure.Producers.Studies;

internal class StudyProducer(
    KafkaDependentProducer<string, string> kafkaDependentProducer)
    : IStudyProducer
{
    public async Task ProduceAsync(Study study, CancellationToken cancellationToken = default)
    {
        var message = new Message<string, string>
        {
            Key = study.Id.ToString(),
            Value = JsonSerializer.Serialize(study.ToInfrastructure())
        };
        await kafkaDependentProducer.ProduceAsync(TopicName, message, cancellationToken);
    }
    
    private const string TopicName = "study_topic";
}