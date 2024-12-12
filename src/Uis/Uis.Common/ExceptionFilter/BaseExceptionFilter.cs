using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Uis.Common.ExceptionFilter;

public abstract class BaseExceptionFilter : IExceptionHandler
{
    protected abstract IResult? HandleException(Exception exception);
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (HandleException(exception) is not { } result)
        {
            return false;
        }
        
        await result.ExecuteAsync(httpContext);
        return true;
    }
}