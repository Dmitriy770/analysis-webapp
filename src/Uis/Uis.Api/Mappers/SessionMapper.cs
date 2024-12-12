using Uis.Domain.Models;
using Response = Uis.Api.Models.Internal;

namespace Uis.Api.Mappers;

internal static class SessionMapper
{
    internal static Response.Session ToResponse(this Session session)
    {
        return new Response.Session(
            SessionId: session.SessionId);
    } 
}