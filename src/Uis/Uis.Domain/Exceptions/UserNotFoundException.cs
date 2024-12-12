namespace Uis.Domain.Exceptions;

public sealed class UserNotFoundException(long userId) : Exception($"User with ID {userId} not found.");