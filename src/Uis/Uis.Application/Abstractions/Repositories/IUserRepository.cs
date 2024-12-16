using Uis.Domain.Models;

namespace Uis.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(long id);
    
    Task AddOrUpdateAsync(User user);
}