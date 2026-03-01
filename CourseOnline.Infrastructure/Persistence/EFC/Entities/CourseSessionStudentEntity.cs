namespace CourseOnline.Infrastructure.Persistence.EFC.Entities;
public class CourseSessionStudentEntity
{
    public StudentEntity Student { get; set; } = null!;
    public Guid StudentId { get; set; }
    public CourseSessionEntity CourseSession { get; set; } = null!;
    public Guid CourseSessionId { get; set; }

}
