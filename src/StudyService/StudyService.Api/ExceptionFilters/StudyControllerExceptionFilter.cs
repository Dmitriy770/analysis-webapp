using Common.Web.ExceptionFilters;
using StudyService.Domain.Exceptions;

namespace StudyService.Api.ExceptionFilters;

internal sealed class StudyControllerExceptionFilter(ILogger<BaseExceptionFilter> logger) : BaseExceptionFilter(logger)
{
    protected override ErrorResponse? HandleException(Exception exception)
    {
        return exception switch
        {
            StudyNotFoundException => new ErrorResponse(
                StatusCode: StatusCodes.Status404NotFound,
                ErrorCode: 0,
                ErrorMessage: exception.Message,
                StackTrace: exception.StackTrace),
            
            StudyResultNotFoundException => new ErrorResponse(
                StatusCode: StatusCodes.Status404NotFound,
                ErrorCode: 0,
                ErrorMessage: exception.Message,
                StackTrace: exception.StackTrace),
            
            _ => null
        };
    }
}