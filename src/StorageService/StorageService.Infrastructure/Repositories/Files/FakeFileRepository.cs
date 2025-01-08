using StorageService.Application.Abstractions.Repositories;

namespace StorageService.Infrastructure.Repositories.Files;

internal sealed class FakeFileRepository : IFileRepository
{
    public async Task UploadAsync(string name, byte[] content, CancellationToken cancellationToken = default)
    {
        Files.Add((name, content));
    }

    public async Task<byte[]?> DownloadAsync(string name, CancellationToken cancellationToken = default)
    {
        var tuple = Files.FirstOrDefault(file => file.FileName == name);
        if (tuple == default)
        {
            return null;
        }
        
        return tuple.Data;
    }

    private static readonly List<(string FileName, byte[] Data)> Files = [];
}