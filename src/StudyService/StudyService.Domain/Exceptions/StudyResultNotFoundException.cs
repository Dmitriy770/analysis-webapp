namespace StudyService.Domain.Exceptions;

public sealed class StudyResultNotFoundException(Guid id) : Exception("Study result with {id} not found.");