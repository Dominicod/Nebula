using Microsoft.EntityFrameworkCore;
using Nebula.Domain.Entities.Networking;

namespace Nebula.Infrastructure.Data;

/// <inheritdoc/>
public sealed class NebulaDbContext : DbContext
{
    /// <inheritdoc/>
    public NebulaDbContext(DbContextOptions<NebulaDbContext> options) : base(options) { }

    /// <summary>
    /// Gets or sets the People DbSet.
    /// </summary>
    public DbSet<Person> People { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NebulaDbContext).Assembly);
    }
}