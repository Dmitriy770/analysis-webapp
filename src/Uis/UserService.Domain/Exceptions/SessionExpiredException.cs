namespace Uis.Domain.Exceptions;

public sealed class SessionExpiredException(Guid sessionId) : Exception($"Session with ID {sessionId} expired.");