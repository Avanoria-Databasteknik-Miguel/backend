using CourseOnline.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.Configurations;

public class StudentCourseEntityConfigurations : IEntityTypeConfiguration<StudentCourseEntity>
{
    public void Configure(EntityTypeBuilder<StudentCourseEntity> builder)
    {
        builder.ToTable("StudentCourses");

        builder.HasKey(x => new { x.StudentId, x.CourseId });


        builder.HasOne(x => x.Student)
            .WithMany(s => s.StudentCourses) 
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(x => x.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(x => x.CourseId);
    }
}
