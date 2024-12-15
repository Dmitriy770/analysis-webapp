﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uis.Api.Filters.Public;
using Uis.Api.Mappers;
using Uis.Api.Models.Public;
using Uis.Application.Commands;
using Uis.Application.Queries;
using Uis.Common.Authorization;
using Uis.Common.Authorization.Attributes;
using Uis.Common.Authorization.Extensions;

namespace Uis.Api.Controllers.Public;

[ApiController]
[Route("user")]
public sealed class UserController(
    ISender sender)
    : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> Login(
        [FromBody] GitHubToken token)
    {
        var (user, session) = await sender.Send(new LoginCommand(token.Token));
        
        HttpContext.AddSessionCookie(session.SessionId);

        return Results.Ok(user.ToUserResponse());
    }

    [HttpGet("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IResult> Logout(
        [FromHeader(Name = Consts.SessionIdKey)]Guid sessionId)
    {
        await sender.Send(new LogoutCommand(sessionId));
        
        return Results.Ok();
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IResult> Get(
        [FromHeader(Name = Consts.SessionIdKey)]Guid sessionId)
    {
        var user = await sender.Send(new GetUserBySessionIdQuery(sessionId));

        return Results.Ok(user.ToUserResponse());
    }
}