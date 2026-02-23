namespace CourseOnline.Infrastructure.Persistence.Entities;

public class CourseCategoryEntity
{
    public CourseEntity Course { get; set; } = null!;
    public Guid CourseId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
    public Guid CategoryId { get; set; }
}
