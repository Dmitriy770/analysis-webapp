using MediatR;
using Microsoft.AspNetCore.Authorization;
using Uis.Application.Commands;

namespace Uis.Api.Filters.Public;

internal sealed class AuthorizationFilter(
    ISender sender)
    : IAuthorizationHandler
{
    public async Task HandleAsync(AuthorizationHandlerContext context)
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