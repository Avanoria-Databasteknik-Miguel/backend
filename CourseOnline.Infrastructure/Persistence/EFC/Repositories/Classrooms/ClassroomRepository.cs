using CourseOnline.Application.Classrooms.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Classrooms;

public sealed class ClassroomRepository(CourseOnlineDbContext context) : RepositoryBase<Classroom, int, ClassroomEntity, CourseOnlineDbContext>(context), IClassroomsRepository
{
    protected override ClassroomEntity ToEntity(Classroom model)
    {
        return new ClassroomEntity
        {
            Id = model.Id,
            Name = model.Name,
            Seats = model.Seats,
            FloorId = model.FloorId
        };
    }

    protected override Classroom ToModel(ClassroomEntity entity)
    {
        return new Classroom(entity.Id, entity.Name, entity.Seats, entity.FloorId);
    }
}
