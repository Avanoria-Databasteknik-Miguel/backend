namespace CourseOnline.Infrastructure.Persistence.EFC.Entities;
public class StudentCourseEntity
{
    public StudentEntity Student { get; set; } = null!;
    public Guid StudentId { get; set; }
    public CourseEntity Course { get; set; } = null!;
    public Guid CourseId { get; set; }
}
