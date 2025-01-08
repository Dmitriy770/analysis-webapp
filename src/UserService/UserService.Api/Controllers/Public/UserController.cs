using Common.Web.Authorization;
using Common.Web.Authorization.Attributes;
using Common.Web.Authorization.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uis.Api.Mappers;
using Uis.Api.Models.Public;
using Uis.Application.Commands;
using Uis.Application.Queries;

namespace Uis.Api.Controllers.Public;

[ApiController]
[Route("user")]
public sealed class UserController(
    ISender sender)
    : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    public async Task<IResult> Login(
        [FromBody] GitHubToken token)
    {
        var (user, session) = await sender.Send(new LoginCommand(token.Token));
        
        HttpContext.AddSessionCookie(session.SessionId);

        return Results.Ok(user.ToUserResponse());
    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IResult> Logout(
        [FromHeader(Name = Const.SessionIdKey)]Guid sessionId)
    {
        await sender.Send(new LogoutCommand(sessionId));
        
        return Results.Ok();
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IResult> Get(
        [FromHeader(Name = Const.SessionIdKey)]Guid sessionId)
    {
        var user = await sender.Send(new GetUserBySessionIdQuery(sessionId));

        return Results.Ok(user.ToUserResponse());
    }
}