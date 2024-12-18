using Common.Configuration.Extensions;
using Microsoft.Extensions.Configuration;

namespace StudyServices.Infrastructure.Settings;

internal sealed class StudyProducerSettings
{
    internal static StudyProducerSettings From(IConfigurationRoot configuration)
    {
        return new StudyProducerSettings
        {
            Servers = configuration.GetRequiredValue<string>("Producers:Study:Servers"),
            Username = configuration.GetRequiredValue<string>("Producers:Study:Username"),
            Password = configuration.GetRequiredValue<string>("Producers:Study:Password"),
            Topic = configuration.GetRequiredValue<string>("Producers:Study:Topic"),
        };
    }
    
    public string Servers { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Topic { get; set; }
    
    private StudyProducerSettings(){}
}