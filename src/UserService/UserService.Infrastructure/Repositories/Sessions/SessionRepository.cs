using System.Text.Json;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Uis.Application.Abstractions.Repositories;
using Uis.Domain.Models;

namespace Uis.Infrastructure.Repositories.Sessions;

internal sealed class SessionRepository(
    IDatabase database,
    ILogger<SessionRepository> logger) : ISessionRepository
{
    public async Task AddAsync(Session session)
    {
        logger.LogInformation("Start adding session {@session}", session);
        
        var key = new RedisKey(session.SessionId.ToString());
        var value = new RedisValue(JsonSerializer.Serialize(session));
        await database.StringAppendAsync(key, value);
        
        key = new RedisKey(session.UserId.ToString());
        value = new RedisValue(session.SessionId.ToString());
        await database.StringAppendAsync(key, value);
        
        logger.LogInformation("End adding session {@session}", session);
    }

    public async Task<Session?> GetAsync(Guid sessionId)
    {
        logger.LogInformation("Start get session {@sessionId}", sessionId);
        
        var key = new RedisKey(sessionId.ToString());
        var value = database.StringGet(key);
        if (!value.HasValue)
        {
            logger.LogInformation("End get session. Session with ID {@sessionId} not found", sessionId);
            return null;
        }
        
        var session = JsonSerializer.Deserialize<Session>(value.ToString());
        logger.LogInformation("End get session {@session}", session);
        return session;
    }

    public async Task DeleteAsync(Guid sessionId)
    {
        logger.LogInformation("Start delete session {@sessionId}", sessionId);
        
        var key = new RedisKey(sessionId.ToString());
        await database.StringGetDeleteAsync(key);
        
        logger.LogInformation("End delete session {@sessionId}", sessionId);
    }

    public async Task DeleteByUserIdAsync(long userId)
    {
        logger.LogInformation("Start delete user {@userId}", userId);
        
        var key = new RedisKey(userId.ToString());
        var value = database.StringGet(key);
        
        key = new RedisKey(value.ToString());
        await database.StringGetDeleteAsync(key);
        
        logger.LogInformation("End delete user {@userId}", userId);
    }
}