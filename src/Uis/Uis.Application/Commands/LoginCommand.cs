using MediatR;
using Uis.Application.Abstractions.Gateways;
using Uis.Application.Abstractions.Providers;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Models;

using User = Uis.Domain.Models.User;

namespace Uis.Application.Commands;

public record LoginCommand(
    string GitHubCode)
    : IRequest<Result>;

internal record Result(
    User User,
    Session Session);

internal sealed class LoginCommandHandler(
    IUserRepository userRepository,
    ISessionRepository sessionRepository,
    IDateTimeProvider dateTimeProvider,
    IGuidProvider guidProvider,
    IGitHubGateway gitHubGateway)
    : IRequestHandler<LoginCommand, Result>
{
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var accessToken = await gitHubGateway.GetAccessTokenAsync(request.GitHubCode);
        
        var gitHubUser = await gitHubGateway.GetUserAsync(accessToken);

        var user = new User(
            Id: gitHubUser.Id,
            Login: gitHubUser.Login,
            Name: gitHubUser.Name,
            AvatarUri: gitHubUser.AvatarUrl,
            Limit: Limit);
        
        await userRepository.AddOrUpdateAsync(user);

        await sessionRepository.DeleteByUserIdAsync(user.Id);
        
        var sessionId = guidProvider.NewGuid();
        var sessionCreationTime = dateTimeProvider.Now;
        var session = new Session(
            SessionId: sessionId,
            UserId: user.Id,
            CreatedDateTime: sessionCreationTime);
        await sessionRepository.AddAsync(session);
        
        return new Result(
            User: user,
            Session: session);
    }

    private const int Limit = 5;
}