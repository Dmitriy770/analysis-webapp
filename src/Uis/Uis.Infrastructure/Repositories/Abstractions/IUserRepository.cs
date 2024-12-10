using Uis.Infrastructure.Repositories.Abstractions.Models;

namespace Uis.Infrastructure.Repositories.Abstractions;

public interface IUserRepository
{
    Task<User?> GetAsync(long id);
    
    Task SaveAsync(User user);
}