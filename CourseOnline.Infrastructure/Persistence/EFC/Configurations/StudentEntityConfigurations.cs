using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using CourseOnline.Infrastructure.Persistence.EFC.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.EFC.Configurations;

public class StudentEntityConfigurations : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.ToTable("Students", t =>
        {
            t.HasCheckConstraint(
                "CK_Students_Email_NotEmpty",
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

        builder.HasOne(x => x.Program)
            .WithMany(p => p.Students)
            .HasForeignKey(x => x.ProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.RowVersion)
            .IsRowVersion()
            .IsRequired();

        builder.ConfigureAuditable();
    }
}
