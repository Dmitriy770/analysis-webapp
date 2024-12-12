using Uis.Application.Abstractions.Providers;

namespace Uis.Infrastructure.Providers;

internal sealed class GuidProvider : IGuidProvider
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}