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
        
        var sessionId = context.GetSessionId();


    }
}