namespace Uis.Domain.Exceptions;

public class SessionNotFound(Guid sessionId) : Exception($"Session ID {sessionId} not found.");