namespace CourseOnline.Infrastructure.Persistence.EFC.Entities;
public class CourseSessionStudentEntity
{
    public StudentEntity Student { get; set; } = null!;
    public Guid StudentId { get; set; }
    public CourseSessionEntity CourseSession = null!;
    public Guid CourseSessionId { get; set; }

}
