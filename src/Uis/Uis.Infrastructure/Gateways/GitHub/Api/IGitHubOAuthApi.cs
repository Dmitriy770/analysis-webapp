﻿using Refit;
using Uis.Infrastructure.Gateways.GitHub.Models;

namespace Uis.Infrastructure.Gateways.GitHub.Api;

internal interface IGitHubOAuthApi
{
    [Get("/login/oauth/access_token")]
    [Headers("X-GitHub-Api-Version: 2022-11-2")]
    Task<AccessTokenInfo> GetAccessTokenAsync(
        [Query]GetAccessTokenParams accessTokenParams);
}