namespace CourseOnline.Infrastructure.Persistence.Entities;
public class ClassroomEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Seats { get; set; }
    public FloorEntity Floor { get; set; } = null!;
    public Guid FloorId { get; set; } 
}
