using MediatR;
using Microsoft.Extensions.Logging;
using Uis.Application.Abstractions.Providers;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Exceptions;
using Uis.Domain.Models;

namespace Uis.Application.Commands;

public record ValidateSession(
    Guid SessionId)
    : IRequest<Session>;

internal sealed class ValidateSessionHandler(
    ISessionRepository sessionRepository,
    IDateTimeProvider dateTimeProvider,
    IGuidProvider guidProvider,
    ILogger<ValidateSessionHandler> logger)
    : IRequestHandler<ValidateSession, Session>
{
    public async Task<Session> Handle(ValidateSession request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start ValidateSession");
        if (await sessionRepository.GetAsync(request.SessionId) is not { } session)
        {
            throw new SessionNotFoundException(request.SessionId);
        }

        var currentDateTime = dateTimeProvider.Now;
        if (currentDateTime < session.CreatedDateTime + _sessionTimeout )
        {
            return session;
        }

        await sessionRepository.DeleteAsync(session.SessionId);
        if (currentDateTime > session.CreatedDateTime + _sessionExtensionTimeout)
        {
            throw new SessionExpiredException(session.SessionId);
        }

        var newSession = new Session(
            SessionId: guidProvider.NewGuid(),
            UserId: session.UserId,
            CreatedDateTime: currentDateTime);
        await sessionRepository.AddAsync(newSession);
        
        logger.LogInformation("End ValidateSession with new {session}", session);
        return newSession;
    }
    
    private readonly TimeSpan _sessionTimeout = TimeSpan.FromMinutes(15);
    private readonly TimeSpan _sessionExtensionTimeout = TimeSpan.FromMinutes(120);
}