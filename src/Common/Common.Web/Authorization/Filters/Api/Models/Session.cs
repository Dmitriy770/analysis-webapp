using Refit;

namespace Common.Web.Authorization.Filters.Api.Models;

public sealed class Session
{
    [AliasAs("sessionId")]
    public Guid SessionId { get; set; }
    
    [AliasAs("userId")]
    public long UserId { get; set; }
}