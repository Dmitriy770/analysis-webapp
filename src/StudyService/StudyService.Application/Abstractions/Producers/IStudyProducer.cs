using StudyService.Domain.Models;
using StudyService.Domain.Models.Studies;

namespace StudyService.Application.Abstractions.Producers;

public interface IStudyProducer
{
    public Task ProduceAsync(Study study, CancellationToken cancellationToken = default);
}