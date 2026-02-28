using CourseOnline.Application.Courses.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Courses;

public class CourseRepository(CourseOnlineDbContext context) : RepositoryBase<Course, Guid, CourseEntity, CourseOnlineDbContext>(context), ICourseRepository
{
    public async Task<Course?> GetByNameAsync(string name, CancellationToken ct)
    {
        var entity = await Context.Courses.AsNoTracking().SingleOrDefaultAsync(x => x.Name == name, ct);
        return entity is null ? null : ToModel(entity);
    }

    protected override CourseEntity ToEntity(Course model)
    {
        return new CourseEntity()
        {
            Id = model.Id,
            Name = model.Name,
            DurationWeeks = model.DurationWeeks,
            MaxStudents = model.MaxStudents
        };
    }

    protected override Course ToModel(CourseEntity entity)
    {
        return new Course(entity.Id, entity.Name, entity.DurationWeeks, entity.MaxStudents, entity.TeacherId, entity.ProgramId);
    }
}
