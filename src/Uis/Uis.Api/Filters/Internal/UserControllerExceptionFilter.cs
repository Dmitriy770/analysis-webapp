using Uis.Common.ExceptionFilter;
using Uis.Domain.Exceptions;

namespace Uis.Api.Filters.Internal;

public sealed class UserControllerExceptionFilter : BaseExceptionFilter
{
    protected override IResult? HandleException(Exception exception)
    {
        return exception switch
        {
            UserNotFoundException => Results.NotFound(),
            SessionNotFoundException => Results.NotFound(),
            _ => null
        };
    }
}