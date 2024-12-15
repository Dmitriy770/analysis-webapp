using MediatR;
using Microsoft.Extensions.Logging;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Exceptions;
using Uis.Domain.Models;

namespace Uis.Application.Queries;

public record GetUserByUserIdQuery(
    long UserId)
    : IRequest<User>;

internal sealed class GetUserByUserIdQueryHandler(
    IUserRepository userRepository,
    ILogger<GetUserByUserIdQueryHandler> logger)
    : IRequestHandler<GetUserByUserIdQuery, User>
{
    public async Task<User> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start handle GetUserByUserIdQuery");
        
        if (await userRepository.GetAsync(request.UserId) is not { } user)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        logger.LogInformation("End handle GetUserByUserIdQuery with {user}", user);
        return user;
    }
} 