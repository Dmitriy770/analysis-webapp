using MediatR;
using Microsoft.Extensions.Logging;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Exceptions;

namespace Uis.Application.Commands;

public record LogoutCommand(
    Guid SessionId)
    : IRequest;

internal sealed class LogoutCommandHandler(
    ISessionRepository sessionRepository,
    ILogger<LogoutCommandHandler> logger)
    : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start handle LogoutCommand");
        
        if(await sessionRepository.GetAsync(request.SessionId) is null)
        {
            throw new SessionNotFoundException(request.SessionId);
        }

        await sessionRepository.DeleteAsync(request.SessionId);
        
        logger.LogInformation("End handle LogoutCommand");
    }
}
    