using Common.Web.Authorization.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Web.Authorization.Filters;

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