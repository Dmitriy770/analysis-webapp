using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using StackExchange.Redis;
using Uis.Application.Abstractions.Repositories;

namespace Uis.Infrastructure.Repositories.Sessions;

internal sealed class SessionRepository(
    IDatabase database) : ISessionRepository
{
    public async Task AddAsync(Domain.Models.Session session)
    {
        await database.JSON().SetAsync(CreateKey(session.SessionId), "$", session);
    }

    public async Task<Domain.Models.Session?> GetAsync(Guid sessionId)
    {
        return await database.JSON().GetAsync<Domain.Models.Session>(CreateKey(sessionId));
    }

    public async Task DeleteAsync(Guid sessionId)
    {
        await database.JSON().DelAsync(CreateKey(sessionId));
    }

    public async Task DeleteByUserIdAsync(long userId)
    {
        var searchResults = await database.FT().SearchAsync(
            "idx:session",
            new Query($"@UserId: {userId}")
                .ReturnFields(new FieldName("$.SessionId, SessionId")));

        foreach (var result in searchResults.Documents.Select(selector => selector["SessionId"]))
        {
            if(Guid.TryParse(result.ToString(), out var sessionId))
            {
                await database.JSON().DelAsync(CreateKey(sessionId));
            }
        }
    }

    private string CreateKey(Guid sessionId)
    {
        return $"session:{sessionId}";
    }
}