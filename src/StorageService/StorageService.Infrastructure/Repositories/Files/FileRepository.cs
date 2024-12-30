using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using StorageService.Application.Abstractions.Repositories;

namespace StorageService.Infrastructure.Repositories.Files;

internal sealed class FileRepository(
    IGridFSBucket bucket)
    : IFileRepository
{
    public async Task UploadAsync(string name, Stream content, CancellationToken cancellationToken = default)
    {
       await bucket.UploadFromStreamAsync(name, content, null, cancellationToken);
    }

    public async Task<Stream?> DownloadAsync(string name, CancellationToken cancellationToken = default)
    {
        var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, name);
        var cursor = await bucket.FindAsync(filter, cancellationToken: cancellationToken);
        var fileInfoList = await cursor.ToListAsync(cancellationToken);
        if (fileInfoList.FirstOrDefault() is not { } doc)
        {
            return null;
        }


        var memoryStream = new MemoryStream();
        await bucket.DownloadToStreamAsync(doc.Id, memoryStream, cancellationToken: cancellationToken);
        return memoryStream;
    }
}