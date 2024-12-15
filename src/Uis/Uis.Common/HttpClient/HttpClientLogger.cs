using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace Uis.Common.HttpClient;

public sealed class HttpClientLogger(
    ILogger<HttpClientLogger> logger)
    : IHttpClientLogger
{
    public object? LogRequestStart(HttpRequestMessage request)
    {
        logger.LogInformation("Request: {Request}", request);
        
        return null;
    }

    public void LogRequestStop(object? context, HttpRequestMessage request, HttpResponseMessage response, TimeSpan elapsed)
    {
        logger.LogInformation("Request end with: {response}. Time: {elapsed}", response, elapsed);
    }

    public void LogRequestFailed(object? context, HttpRequestMessage request, HttpResponseMessage? response, Exception exception,
        TimeSpan elapsed)
    {
        logger.LogInformation("Request failed with {exception}.Response: {response}",exception,  request);
    }
}