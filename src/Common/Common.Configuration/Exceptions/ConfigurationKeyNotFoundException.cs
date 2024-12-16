namespace Common.Configuration.Exceptions;

public sealed class ConfigurationKeyNotFoundException(string key)
    : Exception($"Configuration key not found: {key}.");