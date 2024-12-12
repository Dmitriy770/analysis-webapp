using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uis.Api.Filters.Internal;
using Uis.Api.Mappers;
using Uis.Application.Queries;

namespace Uis.Api.Controllers.Internal;

[ApiController]
[Route("internal/user")]
[ServiceFilter<UserControllerExceptionFilter>]
public class UserController(
    ISender sender)
    : ControllerBase
{
    [HttpGet("/limits/total")]
    public async Task<IResult> GetLimitsBySessionId(
        [FromQuery] Guid sessionId)
    {
        var user = await sender.Send(new GetUserBySessionIdQuery(sessionId));

        return Results.Ok(user.ToLimitResponse());
    }
    
    [HttpGet("{userId:long}/limits/total")]
    public async Task<IResult> GetLimitsBySessionId(
        long userId)
    {
        var user = await sender.Send(new GetUserByUserIdQuery(userId));

        return Results.Ok(user.ToLimitResponse());
    }
}