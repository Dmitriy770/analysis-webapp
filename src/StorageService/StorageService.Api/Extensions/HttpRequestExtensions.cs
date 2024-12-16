namespace StorageService.Api.Extensions;

internal static class HttpRequestExtensions
{
    internal static string GetFileName(this HttpRequest request)
    {
        if (!request.Headers.TryGetValue("Content-Disposition", out var contentDisposition))
        {
            throw new FileNotFoundException();
        }
        
        var contentDispositionStr = contentDisposition.ToString();
        var startInd = contentDispositionStr.IndexOf(StartPrefix);
        var endInd = contentDispositionStr.IndexOf(";", startInd + 1);
        if (startInd == -1 || endInd == -1)
        {
            throw new FileNotFoundException();
        }
        return contentDispositionStr[(startInd + StartPrefix.Length)..endInd];
    }
    
    private const string StartPrefix = "filename=";
}