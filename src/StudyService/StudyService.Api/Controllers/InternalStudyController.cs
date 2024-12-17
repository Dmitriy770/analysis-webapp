using Microsoft.AspNetCore.Mvc;

namespace StudyService.Api.Controllers;

[ApiController]
[Route("internal/studies")]
public sealed class InternalStudyController : ControllerBase
{
    [HttpPost("{id:guid}/result")]
    public Task<IResult> AddResult(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id:guid}")]
    public Task<IResult> Get(Guid id)
    {
        throw new NotImplementedException();
    }
}