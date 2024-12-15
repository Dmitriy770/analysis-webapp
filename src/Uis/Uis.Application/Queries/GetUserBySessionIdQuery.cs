using MediatR;
using Microsoft.Extensions.Logging;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Exceptions;
using Uis.Domain.Models;

namespace Uis.Application.Queries;

public record GetUserBySessionIdQuery(
    Guid SessionId)
    : IRequest<User>;
    
internal sealed class  GetUserBySessionIdHandler(
    IUserRepository userRepository,
    ISessionRepository sessionRepository,
    ILogger<GetUserBySessionIdQuery> logger)
    : IRequestHandler<GetUserBySessionIdQuery, User>
{
    public async Task<User> Handle(GetUserBySessionIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start handle GetUserBySessionIdQuery");
        
        if (await sessionRepository.GetAsync(request.SessionId) is not { } session)
        {
            throw new SessionNotFoundException(request.SessionId);
        }

        if (await userRepository.GetAsync(session.UserId) is not { } user)
        {
            throw new UserNotFoundException(session.UserId);
        }

        logger.LogInformation("End handle GetUserBySessionIdQuery with {user}", user);
        return user;
    }
} 