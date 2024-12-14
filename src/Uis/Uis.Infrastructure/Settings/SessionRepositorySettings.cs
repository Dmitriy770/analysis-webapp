using Microsoft.Extensions.Configuration;
using Uis.Common.Configuration;

namespace Uis.Infrastructure.Settings;

internal sealed class SessionRepositorySettings
{
    public static SessionRepositorySettings From(IConfigurationRoot configuration)
    {
        return new SessionRepositorySettings
        {
            Endpoint = configuration.GetRequiredValue<string>("Repositories:SessionRepository:Endpoint"),
            Port = configuration.GetRequiredValue<int>("Repositories:SessionRepository:Port"),
            User = configuration.GetRequiredValue<string>("Repositories:SessionRepository:User"),
            Password = configuration.GetRequiredValue<string>("Repositories:SessionRepository:Password")
        };
    }
    
    public string Endpoint { get; private set; }
    
    public int Port { get; private set; }
    
    public string User { get; private set; }
    
    public string Password { get; private set; }

    private SessionRepositorySettings(){}
}