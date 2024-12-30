namespace StudyService.Domain.Exceptions;

public class DatasetNotFoundException(string identifier) : Exception($"Dataset with {identifier} not found.")
{
    
}