using CourseOnline.Infrastructure.Common.Entities;

namespace CourseOnline.Infrastructure.Persistence.EFC.Entities;
public class ClassroomEntity : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Seats { get; set; }
    public FloorEntity Floor { get; set; } = null!;
    public int FloorId { get; set; }
    public ICollection<CourseSessionEntity> CourseSessions { get; set; } = [];
}
