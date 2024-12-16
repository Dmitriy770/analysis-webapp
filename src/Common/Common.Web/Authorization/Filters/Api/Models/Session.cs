namespace Common.Web.Authorization.Filters.Api.Models;

internal sealed class Session
{
    internal Guid SessionId { get; set; }
    internal long UserId { get; set; }
}