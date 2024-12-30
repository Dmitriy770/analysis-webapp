using Common.Web.ExceptionFilters;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StorageService.Api.ExceptionFilters;
using StorageService.Api.Mappers;
using StorageService.Api.Models;
using StorageService.Application.Queries;

namespace StorageService.Api.Controllers;

[ApiController]
[Route("internal/datasets")]
[ServiceFilter<InternalDatasetControllerExceptionFilter>]
public sealed class InternalDatasetController(
    ISender sender)
    : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<FileContentHttpResult>(StatusCodes.Status200OK, ContentType)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetContentByIdQuery(id), cancellationToken);
    
        var content = await StreamToBytes(result.Content, cancellationToken);
        await result.Content.DisposeAsync();
        return Results.File(content, ContentType, result.Name);
    }
    
    [HttpGet("{id:guid}/description")]
    [ProducesResponseType<DatasetDescription>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetDescription(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var description = await sender.Send(new GetDescriptionByIdQuery(id), cancellationToken);
        
        return Results.Ok(description.ToApi());
    }
    
    // [HttpGet("description")]
    // [ProducesResponseType<DatasetsDescription>(StatusCodes.Status200OK)]
    // public async Task<IResult> GetDescriptionsByUserId(
    //     [FromQuery(Name = "userId")] long userId,
    //     CancellationToken cancellationToken = default)
    // {
    //     var descriptions = sender.CreateStream(new GetDescriptionsByUserIdQuery(userId), cancellationToken);
    //
    //     return Results.Ok(await descriptions.ToApi(cancellationToken));
    // }
    
    [HttpGet("description")]
    [ProducesResponseType<DatasetsDescription>(StatusCodes.Status200OK)]
    public async Task<IResult> GetDescriptionByUserIdAndName(
        [FromQuery(Name = "userId")] long userId,
        [FromQuery(Name = "datasetName")] string datasetName,
        CancellationToken cancellationToken = default)
    {
        var description = await sender.Send(new GetDescriptionByUserIdAndNameQuery(datasetName, userId), cancellationToken);
    
        return Results.Ok(description.ToApi());
    }

    private async Task<byte[]> StreamToBytes(Stream stream, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
    
    private const string ContentType = "text/csv";
}