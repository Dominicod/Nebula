using Microsoft.Extensions.DependencyInjection;
using Nebula.Services.Networking;

namespace Nebula.Services.Extensions.Dependencies;

/// <summary>
/// Extension methods for registering service dependencies.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddNebulaServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();

        return services;
    }
}
