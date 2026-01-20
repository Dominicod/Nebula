using Microsoft.EntityFrameworkCore;
using Nebula.Domain.Entities.ActionItems;
using Nebula.Domain.Entities.Networking;

namespace Nebula.Infrastructure.Data;

/// <inheritdoc />
internal sealed class NebulaDbContext : DbContext
{
    /// <inheritdoc />
    public NebulaDbContext(DbContextOptions<NebulaDbContext> options) : base(options)
    {
    }

    /// <summary>
    ///     Gets or sets the People DbSet.
    /// </summary>
    public DbSet<Person> People { get; set; }

    /// <summary>
    ///     Gets or sets the ActionItems DbSet.
    /// </summary>
    public DbSet<ActionItem> Tasks { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NebulaDbContext).Assembly);
    }
}