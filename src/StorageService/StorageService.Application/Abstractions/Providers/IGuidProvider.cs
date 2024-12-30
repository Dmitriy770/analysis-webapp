namespace StorageService.Application.Abstractions.Providers;

public interface IGuidProvider
{
    public Guid NewGuid();
}