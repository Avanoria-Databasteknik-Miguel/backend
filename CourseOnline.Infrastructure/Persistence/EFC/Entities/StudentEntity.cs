using CourseOnline.Infrastructure.Common.Entities;

namespace CourseOnline.Infrastructure.Persistence.EFC.Entities;
public class StudentEntity : AuditableEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ProgramEntity? Program { get; set; } 
    public Guid? ProgramId { get; set; }
    public ICollection<StudentCourseEntity> StudentCourses { get; set; } = [];
    public string? ImageUrl { get; set; }
}
