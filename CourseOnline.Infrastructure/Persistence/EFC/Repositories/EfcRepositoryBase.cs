using CourseOnline.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories;

public abstract class EfcRepositoryBase<TEntity, TId, TModel>(DbContext context) : IRepositoryBase<TModel, TId> where TEntity : class, IEntity<TId>
{

    protected DbContext Context { get; } = context;

    protected DbSet<TEntity> Set => Context.Set<TEntity>();



    public abstract TModel ToModel(TEntity entity);

    public abstract Task<TModel> AddASync(TModel model, CancellationToken ct = default);

    public abstract Task<TModel?> UpdateAsync(TId id, TModel model, CancellationToken ct = default);



    public virtual async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await Set.AsNoTracking().ToListAsync(ct);
        return [.. entities.Select(ToModel)];
    }

    public async Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        var entity = await Set.AsNoTracking().SingleOrDefaultAsync(x => x.Id!.Equals(id), ct);
        return entity is null ? default : ToModel(entity);

    }

    public async Task<bool> RemoveAsync(TId id, CancellationToken ct = default)
    {
        var entity = await Set.SingleOrDefaultAsync(x => x.Id!.Equals(id), ct);
        if (entity is null) return false;

        Set.Remove(entity);
        return true;
    }


}
