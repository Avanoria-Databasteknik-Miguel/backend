using CourseOnline.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories;

public abstract class RepositoryBase<TModel, TId, TEntity, TDbContext>(TDbContext context) : IRepositoryBase<TModel, TId> where TEntity : class where TDbContext : DbContext
{

    protected readonly TDbContext Context = context;

    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    protected abstract TEntity ToEntity(TModel model);
    protected abstract TModel ToModel(TEntity entity);


    public virtual async Task<TModel> AddASync(TModel model, CancellationToken ct = default)
    {
        var entity = ToEntity(model);
        await Set.AddAsync(entity, ct);
        await Context.SaveChangesAsync(ct); //Skall göra i Application (Service)??
        return ToModel(entity);
    }

    public virtual async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await Set.AsNoTracking().ToListAsync(ct);
        return [.. entities.Select(ToModel)];
    }

    public virtual async Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        var entity = await Set.FindAsync([id], ct);
        return entity is null ? default : ToModel(entity);
    }

    public virtual async Task<bool> RemoveAsync(TId id, CancellationToken ct = default)
    {
        var entity = await Set.FindAsync([id], ct);
        if (entity is null) return false;

        Set.Remove(entity);
        await Context.SaveChangesAsync(ct); //Skall göra i Application (Service)??
        return true;
    }

    public virtual async Task<TModel?> UpdateAsync(TId id, TModel model, CancellationToken ct = default)
    {
        var entity = await Set.FindAsync([id], ct);

        if (entity is null) return default;

        var updated = ToEntity(model);

        Context.Entry(entity).CurrentValues.SetValues(updated);

        await Context.SaveChangesAsync(ct); //Skall göra i Application(Service)??

        return ToModel(updated);
    }
}
