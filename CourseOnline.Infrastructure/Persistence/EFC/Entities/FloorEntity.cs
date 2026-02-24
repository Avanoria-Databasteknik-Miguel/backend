namespace CourseOnline.Infrastructure.Persistence.EFC.Entities;
public class FloorEntity
{
    public int Id { get; set; }
    public string Level { get; set; } = null!;
    public ICollection<ClassroomEntity> Classrooms { get; set; } = [];
}
