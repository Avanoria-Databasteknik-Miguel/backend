using CourseOnline.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.Configurations;

public sealed class CourseEntityConfigurations : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DurationWeeks)
            .IsRequired(false);


        builder.Property(x => x.MaxStudents)
            .IsRequired(false);

        builder.HasOne(x => x.Teacher)
            .WithMany()
            .HasForeignKey(x => x.TeacherId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Program)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.ProgramId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
