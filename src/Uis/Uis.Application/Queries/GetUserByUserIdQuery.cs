using MediatR;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Exceptions;
using Uis.Domain.Models;

namespace Uis.Application.Queries;

public record GetUserByUserIdQuery(
    long UserId)
    : IRequest<User>;

internal sealed class GetUserByUserIdQueryHandler(
    IUserRepository userRepository)
    : IRequestHandler<GetUserByUserIdQuery, User>
{
    public async Task<User> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        if (await userRepository.GetAsync(request.UserId) is not { } user)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        return user;
    }
} 