using StudyService.Application.Abstractions.Providers;

namespace StudyServices.Infrastructure.Providers;

internal sealed class GuidProvider : IGuidProvider
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}