using StorageService.Application.Abstractions.Providers;

namespace StorageService.Infrastructure.Providers;

internal sealed class GuidProvider : IGuidProvider
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}