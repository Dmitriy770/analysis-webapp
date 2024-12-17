using StudyService.Application.Abstractions.Producers;
using StudyService.Domain.Models.Studies;

namespace StudyServices.Infrastructure.Producers.Studies;

public class StudyProducer : IStudyProducer
{
    public Task ProduceAsync(Study study, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}