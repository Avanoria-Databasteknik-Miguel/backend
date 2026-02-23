using CourseOnline.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.Persistence.Configurations;

public sealed class CourseCategoryEntityConfigurations : IEntityTypeConfiguration<CourseCategoryEntity>
{
    public void Configure(EntityTypeBuilder<CourseCategoryEntity> builder)
    {
        builder.ToTable("CourseCategories");

        builder.HasKey(x => new { x.CourseId, x.CategoryId });

        builder.HasOne(x => x.Course)
            .WithMany(x => x.CourseCategories)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

       builder.HasOne(x => x.Category)
          .WithMany(x => x.CourseCategories) 
          .HasForeignKey(x => x.CategoryId)
          .OnDelete(DeleteBehavior.Cascade);

       builder.HasIndex(x => x.CategoryId);
    }
}
