using Microsoft.AspNetCore.Mvc.Filters;
using Uis.Common.Authorization.Extensions;

namespace Uis.Common.Authorization.Filters;

internal sealed class AuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.IsAuthorizationRequired())
        {
            return;
        }

        if (context.GetSessionId() is not { } sessionId)
        {
            throw new Exception();
        }
        
        
    }
}