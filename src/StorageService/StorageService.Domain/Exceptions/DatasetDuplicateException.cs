namespace StorageService.Domain.Exceptions;

public sealed class DatasetDuplicateException(string name) : Exception($"Dataset with {name} already exists.");