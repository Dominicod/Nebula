using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebula.Architecture.Data.Entities.Configuration.Shared;
using Nebula.Domain.Entities.Networking;

namespace Nebula.Architecture.Data.Entities.Configuration;

/// <summary>
/// EF Core configuration for the <see cref="Person"/> entity.
/// </summary>
internal sealed class PersonConfiguration : BaseEntityConfiguration<Person, Guid>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);
    }
}