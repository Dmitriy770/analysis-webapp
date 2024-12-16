using Common.Web.Authorization.Filters.Api.Models;
using Refit;

namespace Common.Web.Authorization.Filters.Api;

internal interface IUserServiceClient
{
    [Get("/internal/session/{sessionId}/validate")]
    [Headers( "Accept: application/json")]
    internal Task<Session> GetSessionAsync(Guid sessionId);
}