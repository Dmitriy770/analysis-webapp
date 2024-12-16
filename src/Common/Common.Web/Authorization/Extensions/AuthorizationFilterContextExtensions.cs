using Common.Web.Authorization.Exception;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using AuthorizeAttribute = Common.Web.Authorization.Attributes.AuthorizeAttribute;

namespace Common.Web.Authorization.Extensions;

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
    
    public static Guid GetSessionId(this AuthorizationFilterContext context)
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

    public static void AddSessionCookie(this AuthorizationFilterContext context, Guid sessionId)
    {
        context.HttpContext.Response.Cookies.Append(Consts.SessionIdKey, sessionId.ToString());
    }
}