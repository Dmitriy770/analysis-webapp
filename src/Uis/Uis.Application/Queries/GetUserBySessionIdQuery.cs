using MediatR;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Exceptions;
using Uis.Domain.Models;

namespace Uis.Application.Queries;

public record GetUserBySessionIdQuery(
    Guid SessionId)
    : IRequest<User>;
    
internal sealed class  GetUserBySessionIdHandler(
    IUserRepository userRepository,
    ISessionRepository sessionRepository)
    : IRequestHandler<GetUserBySessionIdQuery, User>
{
    public async Task<User> Handle(GetUserBySessionIdQuery request, CancellationToken cancellationToken)
    {
        if (await sessionRepository.GetAsync(request.SessionId) is not { } session)
        {
            throw new SessionNotFoundException(request.SessionId);
        }

        if (await userRepository.GetAsync(session.UserId) is not { } user)
        {
            throw new UserNotFoundException(session.UserId);
        }
        
        return user;
    }
} 