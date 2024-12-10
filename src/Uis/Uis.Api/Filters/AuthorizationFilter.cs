using Microsoft.AspNetCore.Mvc.Filters;

namespace Uis.Api.Filters;

internal sealed class AuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.Cookies.TryGetValue("SessionId", out var sessionId);
        throw new NotImplementedException();
        
        context.ActionDescriptor 
        
        context.ActionDescriptor.FilterDescriptors
    }
}