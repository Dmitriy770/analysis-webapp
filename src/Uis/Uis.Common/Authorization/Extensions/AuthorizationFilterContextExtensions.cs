using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Uis.Common.Authorization.Attributes;

namespace Uis.Common.Authorization.Extensions;

public static class AuthorizationFilterContextExtensions
{
    public static bool IsAuthorizationRequired(this AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor contextActionDescriptor)
        {
            return false;
        }
        
        var isAuthorizationRequired = contextActionDescriptor.MethodInfo
            .GetCustomAttributes(typeof(AuthorizeAttribute), false)
            .Any();

        return isAuthorizationRequired;

    }
    
    public static Guid? GetSessionId(this AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Cookies.TryGetValue(SessionIdKey, out var sessionIdString))
        {
            return null;
        }

        if (Guid.TryParse(sessionIdString, out var sessionId))
        {
            return sessionId;
        }

        return null;
    }

    private const string SessionIdKey = "SessionId";
}