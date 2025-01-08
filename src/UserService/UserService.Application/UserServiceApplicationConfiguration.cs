using Microsoft.Extensions.DependencyInjection;
using Uis.Application.Commands;

namespace Uis.Application;

public static class UserServiceApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<LoginCommand>();
        });

        return serviceCollection;
    }
}