using Refit;

namespace Common.Web.Authorization.Filters.Api.Models;

internal sealed class Session
{
    [AliasAs("sessionId")]
    internal Guid SessionId { get; set; }
    
    [AliasAs("userId")]
    internal long UserId { get; set; }
}