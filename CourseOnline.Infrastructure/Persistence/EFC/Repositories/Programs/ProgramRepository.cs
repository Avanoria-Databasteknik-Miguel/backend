using CourseOnline.Application.Programs.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Programs;

public class ProgramRepository(CourseOnlineDbContext context) : RepositoryBase<Program, Guid, ProgramEntity, CourseOnlineDbContext>(context), IProgramRepository
{
    protected override ProgramEntity ToEntity(Program model)
    {
        return new ProgramEntity()
        {
            Id = model.Id,
            Name = model.Name,
            DurationWeeks = model.DurationWeeks,
            MaxStudents = model.MaxStudents
        };
    }

    protected override Program ToModel(ProgramEntity entity)
    {
        return new Program(entity.Id, entity.Name, entity.DurationWeeks, entity.MaxStudents);
    }

    public async Task<Program?> GetByNameAsync(string name, CancellationToken ct)
    {
        var entity = await Context.Programs.AsNoTracking().SingleOrDefaultAsync(x => x.Name == name, ct);
        return entity is null ? null : ToModel(entity);
    }
}
