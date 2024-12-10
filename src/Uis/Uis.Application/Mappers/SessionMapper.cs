using Uis.Domain.Models;
using Repositories = Uis.Infrastructure.Repositories.Abstractions.Models;

namespace Uis.Application.Mappers;

public static class SessionMapper
{
    public static Repositories.Session ToRepository(this Session session)
    {
        return new Repositories.Session(
            SessionId: session.SessionId,
            UserId: session.UserId,
            session.CreatedDate);
    }
}