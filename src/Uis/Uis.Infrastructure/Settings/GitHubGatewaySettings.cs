﻿using Common.Configuration.Extensions;
using Microsoft.Extensions.Configuration;

namespace Uis.Infrastructure.Settings;

internal sealed class GitHubGatewaySettings
{
    public static GitHubGatewaySettings From(IConfigurationRoot configuration)
    {
        return new GitHubGatewaySettings
        {
            ApiUri = configuration.GetRequiredValue<Uri>("Gateways:GitHub:ApiUri"),
            OauthUri = configuration.GetRequiredValue<Uri>("Gateways:GitHub:OauthUri"),
            RedirectUri = configuration.GetRequiredValue<string>("Gateways:GitHub:RedirectUri"),
            ClientId = configuration.GetRequiredValue<string>("Gateways:GitHub:ClientId"),
            ClientSecret = configuration.GetRequiredValue<string>("Gateways:GitHub:ClientSecret"),
        };
    }
    
    public Uri ApiUri { get; private set; }
    
    public Uri OauthUri { get; private set; }
    
    public string RedirectUri { get; private set; }
    
    public string ClientId { get; private set; }
    
    public string ClientSecret { get; private set; }

    private GitHubGatewaySettings(){}
}
