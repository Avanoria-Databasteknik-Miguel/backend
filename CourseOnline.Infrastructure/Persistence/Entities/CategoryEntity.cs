namespace CourseOnline.Infrastructure.Persistence.Entities;
public class CategoryEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<CourseCategoryEntity> CourseCategories { get; set; } = [];

}
