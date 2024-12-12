using Uis.Domain.Models;

namespace Uis.Application.Abstractions.Repositories;

public interface ISessionRepository
{
    Task AddAsync(Session session);
    
    Task<Session?> GetAsync(Guid sessionId);
    
    Task DeleteAsync(Guid sessionId);
    
    Task DeleteByUserIdAsync(long userId);
    
}