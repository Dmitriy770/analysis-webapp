using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uis.Api.Mappers;
using Uis.Api.Models.Public;
using Uis.Application.Commands;
using Uis.Application.Queries;
using Uis.Common.Authorization;
using Uis.Common.Authorization.Extensions;

namespace Uis.Api.Controllers.Public;

[ApiController]
[Route("user")]
internal sealed class UserController(
    ISender sender)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(
        [FromBody] GitHubToken token)
    {
        var (user, session) = await sender.Send(new LoginCommand(token.Token));
        
        HttpContext.AddSessionCookie(session.SessionId);

        return Results.Ok(user.ToUserResponse());
    }

    [HttpGet("logout")]
    public async Task<IResult> Logout(
        [FromHeader(Name = Consts.SessionIdKey)]Guid sessionId)
    {
        await sender.Send(new LogoutCommand(sessionId));
        
        return Results.Ok();
    }

    [HttpGet]
    public async Task<IResult> Get(
        [FromHeader(Name = Consts.SessionIdKey)]Guid sessionId)
    {
        var user = await sender.Send(new GetUserBySessionIdQuery(sessionId));

        return Results.Ok(user);
    }
}