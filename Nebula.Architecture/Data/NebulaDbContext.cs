using Microsoft.EntityFrameworkCore;
using Nebula.Domain.Entities;

namespace Nebula.Architecture.Data;

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