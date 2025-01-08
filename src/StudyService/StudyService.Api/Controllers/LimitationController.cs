using Common.Web.Authorization.Attributes;
using Common.Web.Authorization.Extensions;
using Common.Web.ExceptionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyService.Api.Mappers;
using StudyService.Api.Models.Limits;
using StudyService.Application.Queries;

namespace StudyService.Api.Controllers;

[ApiController]
[Route("studies/limits")]
public sealed class LimitationController(
    ISender sender)
    : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType<StudyLimits>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IResult> GetUserLimits(CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();

        var limits = await sender.Send(new GetStudyLimitsQuery(userId), cancellationToken);
        
        return Results.Ok(limits.ToApi());
    }
}