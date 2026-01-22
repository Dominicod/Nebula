using Microsoft.Extensions.DependencyInjection;
using Nebula.Contracts.Repositories;
using Nebula.Contracts.Repositories.Networking;
using Nebula.Infrastructure.Data;
using Nebula.Infrastructure.Data.Repositories.Networking;

namespace Nebula.Infrastructure.Extensions.Dependencies;

/// <summary>
///     Extension methods for registering repository data dependencies.
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    ///     Adds application repositories to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static void AddNebulaRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}