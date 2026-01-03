using Nebula.Architecture.Configuration.Shared;

namespace Nebula.Architecture.Configuration;

/// <summary>
/// Configuration options for the Nebula database context.
/// </summary>
internal sealed class NebulaDbContextConfiguration : BaseConfiguration<NebulaDbContextConfiguration>
{
    /// <summary>
    /// The SQL Server connection string.
    /// </summary>
    public required string ConnectionString { get; set; }
}