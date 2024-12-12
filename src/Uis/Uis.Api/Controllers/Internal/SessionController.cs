using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uis.Api.Mappers;
using Uis.Application.Commands;

namespace Uis.Api.Controllers.Internal;

[ApiController]
[Route("internal/session")]
public sealed class SessionController(
    ISender sender)
    : ControllerBase
{
    [HttpGet("{sessionId:guid}/validate")]
    public async Task<IResult> Validate(
        Guid sessionId)
    {
        var session = await sender.Send(new ValidateSession(sessionId));

        return Results.Ok(session.ToResponse());
    }
}