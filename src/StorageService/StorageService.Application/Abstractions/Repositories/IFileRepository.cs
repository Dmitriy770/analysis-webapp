namespace StorageService.Application.Abstractions.Repositories;

public interface IFileRepository
{
    public Task UploadAsync(string name, Stream content, CancellationToken cancellationToken = default);

    public Task<Stream?> DownloadAsync(string name, CancellationToken cancellationToken = default);
    
    public Task DeleteAsync(string name, CancellationToken cancellationToken = default);
}