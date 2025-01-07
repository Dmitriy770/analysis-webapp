using StorageService.Application.Abstractions.Repositories;

namespace StorageService.Infrastructure.Repositories.Files;

internal sealed class FakeFileRepository : IFileRepository
{
    public async Task UploadAsync(string name, Stream content, CancellationToken cancellationToken = default)
    {
        using var ms = new MemoryStream();
        await content.CopyToAsync(ms, cancellationToken);
        
        Files.Add((name, ms.ToArray()));
    }

    public async Task<Stream?> DownloadAsync(string name, CancellationToken cancellationToken = default)
    {
        var tuple = Files.FirstOrDefault(file => file.FileName == name);
        if (tuple == default)
        {
            return null;
        }

        var stream = new MemoryStream();
        await stream.WriteAsync(tuple.Data.AsMemory(0, tuple.Data.Length), cancellationToken);
        
        return stream;
    }

    private static readonly List<(string FileName, byte[] Data)> Files = [];
}