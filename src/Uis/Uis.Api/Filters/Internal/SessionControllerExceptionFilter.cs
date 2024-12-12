using System.Net;
using Uis.Common.ExceptionFilter;
using Uis.Domain.Exceptions;

namespace Uis.Api.Filters.Internal;

public sealed class SessionControllerExceptionFilter : BaseExceptionFilter
{
    protected override ErrorResponse? HandleException(Exception exception)
    {
        return exception switch
        {
            SessionNotFoundException => new ErrorResponse(
                StatusCode: (int)HttpStatusCode.NotFound,
                ErrorCode: 0,
                ErrorMessage: exception.Message),

            SessionExpiredException => new ErrorResponse(
                StatusCode: (int)HttpStatusCode.Forbidden,
                ErrorCode: 0,
                ErrorMessage: exception.Message),

            _ => null
        };
    }
}