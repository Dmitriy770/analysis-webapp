using Common.Web.ExceptionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyService.Api.ExceptionFilters;
using StudyService.Api.Models.StudyResults;
using StudyService.Application.Commands;

namespace StudyService.Api.Controllers;

[ApiController]
[Route("internal/studies")]
[ServiceFilter<InternalStudyControllerExceptionFilter>]
public sealed class InternalStudyController(
    ISender sender)
    : ControllerBase
{
    [HttpPost("{id:guid}/result")]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IResult> AddResult(
        Guid id, 
        StudyResult result,
        CancellationToken cancellationToken)
    {
        await sender.Send(new AddStudyResultCommand(
            StudyId: id,
            Points: result.Points),
            cancellationToken);
        
        return Results.Ok();
    }
}