using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Uis.Application.Commands;
using Uis.Common.Authorization.Extensions;

namespace Uis.Api.Filters.Public;

public sealed class AuthorizationFilter(
    ISender sender)
    : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.IsAuthorizationRequired())
        {
            return;
        }

        var sessionId = context.GetSessionId();
        var newSession = await sender.Send(new ValidateSession(sessionId));
        context.AddSessionToHeader(newSession.SessionId);
    }
}