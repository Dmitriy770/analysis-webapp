using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace Common.Logging.HttpClient;

public sealed class HttpClientLogger(
    ILogger<HttpClientLogger> logger)
    : IHttpClientAsyncLogger
{
    public ValueTask<object?> LogRequestStartAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        logger.LogInformation("Request: {Request}", request);
        
        return ValueTask.FromResult<object?>(null);
    }

    public async ValueTask LogRequestStopAsync(
        object? context,
        HttpRequestMessage request,
        HttpResponseMessage response,
        TimeSpan elapsed,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        logger.LogInformation("Request end with: {response}. Content: {contenent}. Time: {elapsed}", response, content, elapsed);
    }

    public async ValueTask LogRequestFailedAsync(
        object? context,
        HttpRequestMessage request,
        HttpResponseMessage? response,
        Exception exception,
        TimeSpan elapsed,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var content = response is not null ? await response.Content.ReadAsStringAsync(cancellationToken) : null;
        logger.LogInformation("Request failed with {exception}. Response: {response}. Content {content}. Time: {elapsed}", exception,  response, content, elapsed);
    }

    public object? LogRequestStart(HttpRequestMessage request)
    {
        return LogRequestStartAsync(request).GetAwaiter().GetResult();
    }

    public void LogRequestStop(object? context, HttpRequestMessage request, HttpResponseMessage response, TimeSpan elapsed)
    {
        LogRequestStopAsync(context, request, response, elapsed).GetAwaiter().GetResult();
    }

    public void LogRequestFailed(object? context, HttpRequestMessage request, HttpResponseMessage? response, Exception exception,
        TimeSpan elapsed)
    {
        LogRequestFailedAsync(context, request, response, exception, elapsed).GetAwaiter().GetResult();
    }
}