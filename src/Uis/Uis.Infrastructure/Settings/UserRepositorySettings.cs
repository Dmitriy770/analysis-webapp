using Microsoft.Extensions.Configuration;
using Uis.Common.Configuration;

namespace Uis.Infrastructure.Settings;

internal sealed class UserRepositorySettings
{
    public static UserRepositorySettings From(IConfigurationRoot configuration)
    {
        return new UserRepositorySettings
        {
            Endpoint = configuration.GetRequiredValue<string>("Repositories:UserRepository:Endpoint"),
            Port = configuration.GetRequiredValue<int>("Repositories:UserRepository:Port"),
            User = configuration.GetRequiredValue<string>("Repositories:UserRepository:User"),
            Password = configuration.GetRequiredValue<string>("Repositories:UserRepository:Password"),
            Database = configuration.GetRequiredValue<string>("Repositories:UserRepository:Database")
        };
    }
    
    public string Endpoint { get; private set; }
    
    public int Port { get; private set; }
    
    public string User { get; private set; }
    
    public string Password { get; private set; }
    
    public string Database { get; private set; }

    private UserRepositorySettings(){}
}