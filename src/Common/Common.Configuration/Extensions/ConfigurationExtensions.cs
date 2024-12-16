using Common.Configuration.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Common.Configuration.Extensions;

public static class ConfigurationExtensions
{
    public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
    {
        if (configuration.GetValue<string>(key) is null)
        {
            throw new ConfigurationKeyNotFoundException(key);
        }

        if (configuration.GetValue<T>(key) is not {} value)
        {
            throw new ConfigurationCastValueException(key, typeof(T));
        }

        return value;
    }
}