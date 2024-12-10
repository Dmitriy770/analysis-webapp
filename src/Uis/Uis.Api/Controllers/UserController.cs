using Microsoft.AspNetCore.Mvc;
using Uis.Api.Models;

namespace Uis.Api.Controllers;

[ApiController]
[Route("user")]
internal sealed class UserController : ControllerBase
{
    [HttpPost("login")]
    public Task<IResult> Login(
        [FromBody] GitHubToken token)
    {
        return Task.FromResult(Results.Ok());
    }

    [HttpGet("logout")]
    public Task<IResult> Logout()
    {
        
    }

    [HttpGet]
    public Task<IResult> Get()
    {
        
    }
}