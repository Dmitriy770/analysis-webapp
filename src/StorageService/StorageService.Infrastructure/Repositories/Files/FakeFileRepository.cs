using StorageService.Application.Abstractions.Repositories;

namespace StorageService.Infrastructure.Repositories.Files;

internal sealed class FakeFileRepository : IFileRepository
{
    public Task UploadAsync(string name, Stream content, CancellationToken cancellationToken = default)
    {
        Files.Add((name, content));
        
        return Task.CompletedTask;
    }

    public Task<Stream?> DownloadAsync(string name, CancellationToken cancellationToken = default)
    {
        var tuple = Files.FirstOrDefault(file => file.FileName == name);
        if (tuple == default)
        {
            return Task.FromResult<Stream?>(null);
        }

        return Task.FromResult<Stream?>(tuple.Stream);
    }

    private static readonly List<(string FileName, Stream Stream)> Files = [];
}