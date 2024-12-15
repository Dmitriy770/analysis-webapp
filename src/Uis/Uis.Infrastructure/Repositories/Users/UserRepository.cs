using Npgsql;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Models;
using Uis.Infrastructure.Repositories.Users.Models;

namespace Uis.Infrastructure.Repositories.Users;

internal sealed class UserRepository(
    UserRepositoryDbContext dbContext)
    : IUserRepository
{
    public Task<User?> GetAsync(long id)
    {
        if (dbContext.Users.FirstOrDefault(userRecord => userRecord.Id == id) is not {} user)
        {
            return Task.FromResult<User?>(null);
        }
        
        return Task.FromResult<User?>(new User(
            Id: user.Id,
            Login: user.Login,
            Name: user.Name,
            AvatarUri: new Uri(user.AvatarUri),
            Limit: user.Limit));
    }

    public async Task AddOrUpdateAsync(User user)
    {
        var findUser = dbContext.Users.FirstOrDefault(userRecord => userRecord.Id == user.Id);
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
    }
    
    protected async Task<NpgsqlConnection> GetAndOpenConnection()
    {
        var connection = new NpgsqlConnection();
        

        await connection.OpenAsync();
        await connection.ReloadTypesAsync();
        return connection;
    } 
}