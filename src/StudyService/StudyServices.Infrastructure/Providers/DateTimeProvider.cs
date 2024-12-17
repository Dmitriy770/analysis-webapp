using StudyService.Application.Abstractions.Providers;

namespace StudyServices.Infrastructure.Providers;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}