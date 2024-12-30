namespace Common.Configuration.Exceptions;

public class ConfigurationCastValueException(string key, Type to) 
    : Exception($"Configuration can not convert value to {to} of key {key}.");