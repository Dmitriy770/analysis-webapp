using Common.Web.ExceptionFilters;
using StorageService.Domain.Exceptions;

namespace StorageService.Api.ExceptionFilters;

internal sealed class InternalDatasetControllerExceptionFilter(
    ILogger<BaseExceptionFilter> logger)
    : BaseExceptionFilter(logger)
{
    protected override ErrorResponse? HandleException(Exception exception)
    {
        return exception switch
        {
            DatasetNotFoundException => new ErrorResponse(
                StatusCode: StatusCodes.Status404NotFound,
                ErrorCode: 0,
                ErrorMessage: exception.Message,
                StackTrace: exception.StackTrace),

            _ => null
        };
    }
}