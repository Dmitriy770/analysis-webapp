namespace Uis.Domain.Exceptions;

public sealed class SessionNotFoundException(Guid sessionId) : Exception($"Session ID {sessionId} not found.");