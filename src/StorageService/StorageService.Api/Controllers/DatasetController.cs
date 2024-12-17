using Common.Web.Authorization.Attributes;
using Common.Web.Authorization.Extensions;
using Common.Web.ExceptionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageService.Api.ExceptionFilters;
using StorageService.Api.Extensions;
using StorageService.Api.Mappers;
using StorageService.Api.Models;
using StorageService.Application.Commands;
using StorageService.Application.Queries;

namespace StorageService.Api.Controllers;

[ApiController]
[Route("datasets")]
[Authorize]
[ServiceFilter<DatasetControllerExceptionFilter>]
public sealed class DatasetController(
    ISender sender)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<DatasetDescription>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IResult> Add(
        CancellationToken cancellationToken)
    {
        
        using var content = new MemoryStream();
        await Request.Body.CopyToAsync(content, cancellationToken);

        var fileName = Request.GetFileName();
        var userId = HttpContext.GetUserId();

        var description = await sender.Send(new AddDatasetCommand(fileName, userId, content), cancellationToken);

        return Results.Ok(description.ToApi());

    }

    [HttpGet("descriptions")]
    [ProducesResponseType<DatasetDescription>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetDescriptionByUserId(
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();

        var descriptions = sender.CreateStream(new GetDescriptionsByUserIdQuery(userId), cancellationToken);

        return Results.Ok(await descriptions.ToApi(cancellationToken));
    }
    
    [HttpGet("{datasetName}/descriptions")]
    [ProducesResponseType<DatasetDescription>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetDescriptionByName(
        string datasetName,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();

        var descriptions = await sender.Send(new GetDescriptionByUserIdAndNameQuery(
            Name: datasetName,
            UserId: userId),
            cancellationToken);
        
        return Results.Ok(descriptions.ToApi());
    }
}