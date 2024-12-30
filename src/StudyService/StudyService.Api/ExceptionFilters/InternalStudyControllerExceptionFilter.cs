using Common.Web.ExceptionFilters;
using StudyService.Domain.Exceptions;

namespace StudyService.Api.ExceptionFilters;

public class InternalStudyControllerExceptionFilter(ILogger<BaseExceptionFilter> logger) : BaseExceptionFilter(logger)
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
            
            StudyAlreadyDoneException => new ErrorResponse(
                StatusCode: StatusCodes.Status400BadRequest,
                ErrorCode: 0,
                ErrorMessage: exception.Message,
                StackTrace: exception.StackTrace),
            
            _ => null
        };
    }
}