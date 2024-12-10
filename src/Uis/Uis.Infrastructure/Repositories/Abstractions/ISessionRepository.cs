using Uis.Infrastructure.Repositories.Abstractions.Models;

namespace Uis.Infrastructure.Repositories.Abstractions;

public interface ISessionRepository
{
    Task SaveAsync(Session session);
    
    Task<Session?> Get(Guid sessionId);
    
    Task DeleteByUserIdAsync(long userId);
}