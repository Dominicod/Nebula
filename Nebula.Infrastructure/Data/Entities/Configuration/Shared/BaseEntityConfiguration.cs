using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebula.Domain.Entities.Common;

namespace Nebula.Infrastructure.Data.Entities.Configuration.Shared;

/// <summary>
///     Base EF Core configuration for entities inheriting from <see cref="BaseEntity{TId}" />.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
internal abstract class BaseEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<TId>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        ConfigureEntity(builder);
    }

    /// <summary>
    ///     Configures the entity-specific properties. Called after base configuration is applied.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}