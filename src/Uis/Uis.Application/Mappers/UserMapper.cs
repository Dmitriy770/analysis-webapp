using Uis.Domain.Models;

using Gateways = Uis.Infrastructure.Gateways.Abstractions.Models;
using Repositories = Uis.Infrastructure.Repositories.Abstractions.Models;

namespace Uis.Application.Mappers;

public static class UserMapper
{
    public static User ToDomain(this Gateways.User user)
    {
        return new User(
            Id: user.Id,
            Login: user.Login,
            AvatarUri: user.AvatarUrl);
    }

    public static Repositories.User ToRepository(this User user)
    {
        return new Repositories.User(
            Id: user.Id,
            Login: user.Login,
            AvatarUri: user.AvatarUri,
            Limit: user.Limit);
    }
}