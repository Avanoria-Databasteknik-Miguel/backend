using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using CourseOnline.Infrastructure.Persistence.EFC.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.EFC.Configurations;

public class TeacherEntityConfigurations : IEntityTypeConfiguration<TeacherEntity>
{
    public void Configure(EntityTypeBuilder<TeacherEntity> builder)
    {
        builder.ToTable("Teachers", t =>
        {
            t.HasCheckConstraint(
                "CK_Teachers_Email_NotEmpty",
                "LEN(LTRIM(RTRIM([Email]))) > 0"
            );
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.ConfigureAuditable();

        //builder.HasIndex(x => new { x.FirstName, x.LastName });
    }
}
