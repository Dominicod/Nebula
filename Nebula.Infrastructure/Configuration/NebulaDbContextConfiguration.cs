using Nebula.Infrastructure.Configuration.Shared;

namespace Nebula.Infrastructure.Configuration;

/// <summary>
/// Configuration options for the Nebula database context.
/// </summary>
internal sealed class NebulaDbContextConfiguration : BaseConfiguration<NebulaDbContextConfiguration>
{
    /// <summary>
    /// The SQL Server connection string.
    /// </summary>
    public required string ConnectionString { get; init; }
}