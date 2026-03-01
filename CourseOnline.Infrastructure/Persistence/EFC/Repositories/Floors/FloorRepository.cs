using CourseOnline.Application.Floors.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Floors;

public class FloorRepository(CourseOnlineDbContext context) : RepositoryBase<Floor, int, FloorEntity, CourseOnlineDbContext>(context), IFloorRepository
{
    public async Task<Floor?> GetByLevelAsync(string level, CancellationToken ct)
    {
        var normalized = level.Trim().ToLower();

        var entity = await Context.Floors
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Level == normalized, ct);

        return entity is null ? null : ToModel(entity);
    }

    protected override FloorEntity ToEntity(Floor model) => new()
    {
        Id = model.Id,
        Level = model.Level
    };

    protected override Floor ToModel(FloorEntity entity) => new(
        id: entity.Id,
        level: entity.Level
    );
}
