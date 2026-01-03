using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nebula.Architecture.Configuration;
using Nebula.Architecture.Data;

namespace Nebula.Architecture.Extensions.Dependencies;

/// <summary>
/// Extension methods for registering SQL data dependencies.
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    /// Adds the Nebula SQL Server database context and related services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddNebulaSql(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = configuration
            .GetSection(NebulaDbContextConfiguration.SectionName)
            .Get<NebulaDbContextConfiguration>();

        ArgumentNullException.ThrowIfNull(dbConfig);

        services.AddDbContext<NebulaDbContext>(options =>
            options.UseSqlServer(dbConfig.ConnectionString));

        return services;
    }
}