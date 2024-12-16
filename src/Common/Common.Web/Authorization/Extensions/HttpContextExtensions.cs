using Microsoft.AspNetCore.Http;

namespace Common.Web.Authorization.Extensions;

public static class HttpContextExtensions
{
    public static void AddSessionCookie(this HttpContext httpContext, Guid sessionId)
    {
        httpContext.Response.Cookies.Append(Consts.SessionIdKey, sessionId.ToString());
    }
}