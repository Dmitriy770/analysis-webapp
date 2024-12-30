using Common.Configuration.Extensions;
using Microsoft.Extensions.Configuration;

namespace StorageService.Infrastructure.Settings;

internal sealed class MongoSettings
{
    internal static MongoSettings From(IConfigurationRoot configuration)
    {
        return new MongoSettings
        {
            Host = configuration.GetRequiredValue<string>("Repositories:MongoRepository:Host"),
            Port = configuration.GetRequiredValue<int>("Repositories:MongoRepository:Port"),
            Database = configuration.GetRequiredValue<string>("Repositories:MongoRepository:Database"),
            BucketName = configuration.GetRequiredValue<string>("Repositories:MongoRepository:BucketName"),
            Username = configuration.GetRequiredValue<string>("Repositories:MongoRepository:Username"),
            Password = configuration.GetRequiredValue<string>("Repositories:MongoRepository:Password"),
        };
    }
    
    public string Host { get; set; }
    
    public int Port { get; set; }
    
    public string Database { get; set; }
    
    public string BucketName { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
}