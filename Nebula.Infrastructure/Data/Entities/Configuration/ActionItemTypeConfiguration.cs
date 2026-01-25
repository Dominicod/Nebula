using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebula.Domain.Entities.ActionItemTypes;
using Nebula.Infrastructure.Data.Entities.Configuration.Shared;

namespace Nebula.Infrastructure.Data.Entities.Configuration;

/// <summary>
///     EF Core configuration for the <see cref="ActionItemType" /> entity.
/// </summary>
internal sealed class ActionItemTypeConfiguration : BaseEntityConfiguration<ActionItemType, Guid>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<ActionItemType> builder)
    {
        builder.ToTable("ActionItemTypes");

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
