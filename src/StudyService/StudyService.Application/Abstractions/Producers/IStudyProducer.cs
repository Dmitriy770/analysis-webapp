using StudyService.Domain.Models;

namespace StudyService.Application.Abstractions.Producers;

public interface IStudyProducer
{
    public Task ProduceAsync(Study study, CancellationToken cancellationToken = default);
}