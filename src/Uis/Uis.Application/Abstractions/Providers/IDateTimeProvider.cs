namespace Uis.Application.Abstractions.Providers;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
}