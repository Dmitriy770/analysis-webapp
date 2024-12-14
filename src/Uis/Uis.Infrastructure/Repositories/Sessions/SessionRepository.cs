using System.Text.Json;
using StackExchange.Redis;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Models;

namespace Uis.Infrastructure.Repositories.Sessions;

internal sealed class SessionRepository(
    IDatabase database) : ISessionRepository
{
    public async Task AddAsync(Session session)
    {
        var key = new RedisKey(session.SessionId.ToString());
        var value = new RedisValue(JsonSerializer.Serialize(session));
        await database.StringAppendAsync(key, value);
        
        key = new RedisKey(session.UserId.ToString());
        value = new RedisValue(session.SessionId.ToString());
        await database.StringAppendAsync(key, value);
    }

    public async Task<Session?> GetAsync(Guid sessionId)
    {
        var key = new RedisKey(sessionId.ToString());
        var value = database.StringGet(key);
        
        return JsonSerializer.Deserialize<Session>(value.ToString());
    }

    public async Task DeleteAsync(Guid sessionId)
    {
        var key = new RedisKey(sessionId.ToString());
        await database.StringGetDeleteAsync(key);
    }

    public async Task DeleteByUserIdAsync(long userId)
    {
        var key = new RedisKey(userId.ToString());
        var value = database.StringGet(key);
        
        key = new RedisKey(value.ToString());
        await database.StringGetDeleteAsync(key);
    }
}