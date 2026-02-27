using CourseOnline.Infrastructure.Common.Entities;

namespace CourseOnline.Infrastructure.Persistence.EFC.Entities;
public class TeacherEntity : AuditableEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ImageUrl { get; set; } = null!;

}
