using Microsoft.Extensions.Logging;
using StudyService.Application.Abstractions.Producers;
using StudyService.Domain.Models.Studies;
using StudyServices.Infrastructure.Repositories.Studies;

namespace StudyServices.Infrastructure.Producers.Studies;

internal sealed class FakeStudyProducer(
    ILogger<FakeStudyRepository> logger) : IStudyProducer
{
    public Task ProduceAsync(Study study, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Produce {@study}", study);
        
        return Task.CompletedTask;
    }
}