using Common.Web.Authorization.Attributes;
using Common.Web.Authorization.Extensions;
using Common.Web.ExceptionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyService.Api.ExceptionFilters;
using StudyService.Api.Mappers;
using StudyService.Api.Models.Studies;
using StudyService.Api.Models.StudyResults;
using StudyService.Application.Commands;
using StudyService.Application.Queries;

namespace StudyService.Api.Controllers;

[ApiController]
[Route("studies")]
[ServiceFilter<StudyControllerExceptionFilter>]
public sealed class StudyController(
    ISender sender)
    : ControllerBase
{
    [Authorize]
    [HttpPost]
    [ProducesResponseType<Study>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IResult> Add(
        NewStudy study,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();

        var createdStudy = await sender.Send(new CreateClusteringStudyCommand(
            NewStudy: study.ToDomain(),
            UserId: userId),
            cancellationToken);

        return Results.Ok(createdStudy.ToApi());
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType<StudiesContainer>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IResult> GetByUserId(CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();

       var studies = sender.CreateStream(new GetStudiesByUserIdQuery(userId), cancellationToken);

       return Results.Ok(await studies.ToApi(cancellationToken));
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    [ProducesResponseType<Study>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetByStudyId(
        Guid id,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();

        var study = await sender.Send(new GetStudyQuery(
            StudyId: id,
            UserId: userId),
            cancellationToken);

        return Results.Ok(study.ToApi());
    }


    [Authorize]
    [HttpGet("{id:guid}/result")]
    [ProducesResponseType<StudyResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetResult(
        Guid id,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();
        
        var studyResult = await sender.Send(new GetStudyResultQuery(
            StudyId: id, 
            UserId:userId),
            cancellationToken);
        
        return Results.Ok(studyResult.ToApi());
    }
}