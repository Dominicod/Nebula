using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nebula.Infrastructure.Configuration;
using Nebula.Infrastructure.Data;

namespace Nebula.Infrastructure.Extensions.Dependencies;

/// <summary>
///     Extension methods for registering SQL data dependencies.
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    ///     Adds the Nebula SQL Server database context and related services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = configuration
            .GetSection(NebulaDbContextConfiguration.SectionName)
            .Get<NebulaDbContextConfiguration>();

        ArgumentNullException.ThrowIfNull(dbConfig);

        services.AddDbContext<NebulaDbContext>(options =>
            options.UseAzureSql(dbConfig.ConnectionString));
    }
}