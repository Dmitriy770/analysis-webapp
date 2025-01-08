using Uis.Domain.Models;

using ResponsePublic = Uis.Api.Models.Public;
using ResponseInternal = Uis.Api.Models.Internal;

namespace Uis.Api.Mappers;

internal static class UserMapper
{
    internal static ResponsePublic.User ToUserResponse(this User user)
    {
        return new ResponsePublic.User(
            Nickname: user.Name,
            AvatarUrl: user.AvatarUri);
    }

    internal static ResponseInternal.Limit ToLimitResponse(this User user)
    {
        return new ResponseInternal.Limit(
            Total: user.Limit);
    }
}