using Common.Web.ExceptionFilters;
using StorageService.Domain.Exceptions;

namespace StorageService.Api.ExceptionFilters;

internal sealed class DatasetControllerExceptionFilter(
    ILogger<BaseExceptionFilter> logger)
    : BaseExceptionFilter(logger)
{
    protected override ErrorResponse? HandleException(Exception exception)
    {
        return exception switch
        {
            DatasetDuplicateException => new ErrorResponse(
                StatusCode: StatusCodes.Status400BadRequest,
                ErrorCode: 0,
                ErrorMessage: exception.Message,
                StackTrace: exception.StackTrace),

            DatasetNotFoundException => new ErrorResponse(
                StatusCode: StatusCodes.Status404NotFound,
                ErrorCode: 0,
                ErrorMessage: exception.Message,
                StackTrace: exception.StackTrace),
            
            _ => null
        };
    }
}