using System.Text.Json;
using Confluent.Kafka;
using StudyService.Application.Abstractions.Producers;
using StudyService.Domain.Models.Studies;
using StudyServices.Infrastructure.Common.Kafka;
using StudyServices.Infrastructure.Producers.Studies.Mappers;
using StudyServices.Infrastructure.Producers.Studies.Models;
using StudyServices.Infrastructure.Settings;

namespace StudyServices.Infrastructure.Producers.Studies;

internal class StudyProducer(
    KafkaDependentProducer<string, string> kafkaDependentProducer,
    StudyProducerSettings settings)
    : IStudyProducer
{
    public async Task ProduceAsync(Study study, CancellationToken cancellationToken = default)
    {
        var t = new JsonDeserializer();
        var message = new Message<string, string>
        {
            Key = study.Id.ToString(),
            Value = JsonSerializer.Serialize(study.ToInfrastructure())
        };
        await kafkaDependentProducer.ProduceAsync(settings.Topic, message, cancellationToken);
    }

}