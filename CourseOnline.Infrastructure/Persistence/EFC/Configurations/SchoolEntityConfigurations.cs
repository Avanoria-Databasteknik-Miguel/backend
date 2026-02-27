using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using CourseOnline.Infrastructure.Persistence.EFC.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.EFC.Configurations;

public sealed class SchoolEntityConfigurations : IEntityTypeConfiguration<SchoolEntity>
{
    public void Configure(EntityTypeBuilder<SchoolEntity> builder)
    {
        builder.ToTable("Schools", t =>
            {
                t.HasCheckConstraint(
                    "CK_Schools_Name_NotEmpty",
                    "LEN(LTRIM(RTRIM([Name]))) > 0"
                );

                t.HasCheckConstraint(
                    "CK_Schools_StreetName_NotEmpty",
                    "LEN(LTRIM(RTRIM([StreetName]))) > 0"
                );
            }
        );

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.StreetName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.HouseNumber)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.ZipCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.PostalTown)
            .HasMaxLength(100)
            .IsRequired();

        builder.ConfigureAuditable();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}
