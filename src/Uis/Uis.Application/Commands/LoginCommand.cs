using MediatR;
using Uis.Application.Mappers;
using Uis.Domain.Models;
using Uis.Infrastructure.Gateways.Abstractions;
using Uis.Infrastructure.Gateways.Abstractions.Models;
using Uis.Infrastructure.Helpers.Abstractions;
using Uis.Infrastructure.Repositories.Abstractions;
using Uis.Infrastructure.Settings;

using User = Uis.Domain.Models.User;

namespace Uis.Application.Commands;

public record LoginCommand(
    string GitHubCode)
    : IRequest<Result>;

public record Result(
    User User,
    Session Session);

internal sealed class LoginCommandHandler(
    IUserRepository userRepository,
    ISessionRepository sessionRepository,
    IDateTimeProvider dateTimeProvider,
    IGuidProvider guidProvider,
    IGitHubGateway gitHubGateway,
    GitHubGatewaySettings gitHubGatewaySettings)
    : IRequestHandler<LoginCommand, Result>
{
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var oauth = new OAuth(
            ClientId: gitHubGatewaySettings.ClientId,
            ClientSecret: gitHubGatewaySettings.ClientSecret,
            RedirectUri: gitHubGatewaySettings.RedirectUri,
            Code: request.GitHubCode);
        var accessToken = await gitHubGateway.GetAccessTokenAsync(oauth);
        
        var gitHubUser = await gitHubGateway.GetUserAsync(accessToken);
        var user = gitHubUser.ToDomain();
        await userRepository.SaveAsync(user.ToRepository());

        await sessionRepository.DeleteByUserIdAsync(user.Id);
        
        var sessionId = guidProvider.NewGuid();
        var sessionCreationTime = dateTimeProvider.Now;
        var session = new Session(
            SessionId: sessionId,
            UserId: user.Id,
            CreatedDate: sessionCreationTime);
        await sessionRepository.SaveAsync(session.ToRepository());
        
        return new Result(
            User: user,
            Session: session);
    }
}