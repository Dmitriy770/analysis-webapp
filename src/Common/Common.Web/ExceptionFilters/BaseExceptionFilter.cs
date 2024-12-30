using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Common.Web.ExceptionFilters;

public abstract class BaseExceptionFilter(
    ILogger<BaseExceptionFilter> logger)
    : IExceptionFilter
{
    protected abstract ErrorResponse? HandleException(Exception exception);
    
    public void OnException(ExceptionContext context)
    {
        if (HandleException(context.Exception) is not { } response)
        {
            response = new ErrorResponse(
                StatusCode: (int)HttpStatusCode.InternalServerError,
                ErrorCode: 0,
                ErrorMessage: context.Exception.Message,
                StackTrace: context.Exception.StackTrace);
        }

        context.Result = new ContentResult
        {
            StatusCode = response.StatusCode,
            Content = JsonSerializer.Serialize(response),
            ContentType = "application/json"
        };
        context.ExceptionHandled = true;
        
        logger.LogInformation("Handling exception and return error {@response}", response);
    }
}