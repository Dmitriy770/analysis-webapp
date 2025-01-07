namespace StorageService.Application.Abstractions.Repositories;

public interface IFileRepository
{
    public Task UploadAsync(string name, byte[] content, CancellationToken cancellationToken = default);

    public Task<byte[]?> DownloadAsync(string name, CancellationToken cancellationToken = default);
}