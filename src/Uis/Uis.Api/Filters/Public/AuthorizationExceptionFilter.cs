using System.Net;
using Uis.Common.ExceptionFilter;
using Uis.Domain.Exceptions;

namespace Uis.Api.Filters.Public;

internal sealed class AuthorizationExceptionFilter : BaseExceptionFilter
{
    protected override ErrorResponse? HandleException(Exception exception)
    {
        return exception switch
        {
            SessionNotFoundException => new ErrorResponse(
                StatusCode: (int)HttpStatusCode.Unauthorized,
                ErrorCode: 0,
                ErrorMessage: exception.Message,
                StackTrace: exception.StackTrace),
            
            _ => null
        };
    }
}