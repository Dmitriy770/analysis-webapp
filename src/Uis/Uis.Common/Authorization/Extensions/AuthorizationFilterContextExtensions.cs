using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Uis.Domain.Exceptions;
using AuthorizeAttribute = Uis.Common.Authorization.Attributes.AuthorizeAttribute;

namespace Uis.Common.Authorization.Extensions;

public static class AuthorizationFilterContextExtensions
{
    public static bool IsAuthorizationRequired(this AuthorizationHandlerContext context)
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
    
    public static Guid GetSessionId(this AuthorizationHandlerContext context)
    {
        if (!context.HttpContext.Request.Cookies.TryGetValue(Consts.SessionIdKey, out var sessionIdString))
        {
            throw new SessionIdNotFoundException();
        }

        if (!Guid.TryParse(sessionIdString, out var sessionId))
        {
            throw new SessionIdNotFoundException();
        }

        return sessionId;
    }

    public static void AddSessionToHeader(this AuthorizationFilterContext context, Guid sessionId)
    {
        context.HttpContext.Request.Headers[Consts.SessionIdKey] = sessionId.ToString();
    }
}