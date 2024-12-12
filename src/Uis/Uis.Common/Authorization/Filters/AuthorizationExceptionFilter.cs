using Microsoft.AspNetCore.Mvc.Filters;

namespace Uis.Common.Authorization.Filters;

public sealed class AuthorizationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = context.Exception switch
        {
            _ => null
        };

        if (result is not null)
        {
            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}