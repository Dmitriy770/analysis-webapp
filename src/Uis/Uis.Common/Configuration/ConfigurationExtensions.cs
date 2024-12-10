using Microsoft.Extensions.Configuration;
using Uis.Common.Configuration.Exceptions;

namespace Uis.Common.Configuration;

public static class ConfigurationExtensions
{
    public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
    {
        if (configuration.GetSection(key).Value is not {} valueString)
        {
            throw new ConfigurationKeyNotFoundException(key);
        }

        if (valueString is not T value)
        {
            throw new ConfigurationCastValueException(key, typeof(T));
        }

        return value;
    }
}