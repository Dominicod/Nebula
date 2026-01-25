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

        // Seed default action item types
        builder.HasData(
            new ActionItemType
            {
                Id = new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                Name = "Home"
            },
            new ActionItemType
            {
                Id = new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"),
                Name = "Work"
            }
        );
    }
}
