namespace StudyService.Domain.Exceptions;

public sealed class StudyAlreadyDoneException(string identifier) : Exception($"Study with {identifier} already done.");