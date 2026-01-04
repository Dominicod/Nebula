using Microsoft.Extensions.DependencyInjection;
using Nebula.Architecture.Repositories.Networking;

namespace Nebula.Architecture.Extensions.Dependencies;

/// <summary>
/// Extension methods for registering repository dependencies.
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    /// Adds repository services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddNebulaRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}