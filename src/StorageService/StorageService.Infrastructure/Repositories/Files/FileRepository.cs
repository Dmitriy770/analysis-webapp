using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using StorageService.Application.Abstractions.Repositories;

namespace StorageService.Infrastructure.Repositories.Files;

internal sealed class FileRepository(
    IGridFSBucket bucket)
    : IFileRepository
{
    public async Task UploadAsync(string name, byte[] content, CancellationToken cancellationToken = default)
    {
        await bucket.UploadFromBytesAsync(name, content, null, cancellationToken);
    }

    public async Task<byte[]?> DownloadAsync(string name, CancellationToken cancellationToken = default)
    {
        var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, name);
        var cursor = await bucket.FindAsync(filter, cancellationToken: cancellationToken);
        var fileInfoList = await cursor.ToListAsync(cancellationToken);
        if (fileInfoList.FirstOrDefault() is not { } doc)
        {
            return null;
        }
        
        var content = await bucket.DownloadAsBytesAsync(doc.Id, cancellationToken: cancellationToken);
        return content;
    }
}