namespace StudyService.Domain.Exceptions;

public class StudyNotFoundException(string identifier) : Exception($"Study with {identifier} not found.")
{
    
}