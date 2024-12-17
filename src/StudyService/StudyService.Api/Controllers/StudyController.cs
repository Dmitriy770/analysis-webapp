using Microsoft.AspNetCore.Mvc;
using StudyService.Api.Models;

namespace StudyService.Api.Controllers;

[ApiController]
[Route("studies")]
public sealed class StudyController : ControllerBase
{
    [HttpPost]
    public Task<IResult> Add(NewStudy study)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<IResult> Get(
        [FromQuery] string datasetName)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<IResult> GetByUserId([FromQuery] Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}/result")]
    public Task<IResult> GetResult(Guid id)
    {
        throw new NotImplementedException();
    }
}