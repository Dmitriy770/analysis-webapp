using Uis.Application.Abstractions.Providers;

namespace Uis.Infrastructure.Providers;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}