using Microsoft.Extensions.Logging;
using Npgsql;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Models;
using Uis.Infrastructure.Repositories.Users.Models;

namespace Uis.Infrastructure.Repositories.Users;

internal sealed class UserRepository(
    UserRepositoryDbContext dbContext,
    ILogger<UserRepository> logger)
    : IUserRepository
{
    public Task<User?> GetAsync(long id)
    {
        logger.LogInformation("Start get user with id: {id}", id);
        
        if (dbContext.Users.FirstOrDefault(userRecord => userRecord.Id == id) is not {} user)
        {
            return Task.FromResult<User?>(null);
        }
        
        logger.LogInformation("End get user with id: {id}", id);
        return Task.FromResult<User?>(new User(
            Id: user.Id,
            Login: user.Login,
            Name: user.Name,
            AvatarUri: new Uri(user.AvatarUri),
            Limit: user.Limit));
    }

    public async Task AddOrUpdateAsync(User user)
    {
        logger.LogInformation("Start add or update user with id: {id}", user.Id);

        var findUser = await dbContext.Users.FindAsync(user.Id);
        if (findUser is null)
        {
            var newUser = new UserStorageRecord
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                AvatarUri = user.AvatarUri.ToString(),
                Limit = user.Limit
            };

            await dbContext.Users.AddAsync(newUser);
        }
        else
        {
            findUser.Login = user.Login;
            findUser.Name = user.Name;
            findUser.AvatarUri = user.AvatarUri.ToString();
            findUser.Limit = user.Limit;
        }

        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("End add or update user with id: {id}", user.Id);
    }
}