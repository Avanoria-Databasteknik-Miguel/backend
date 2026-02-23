namespace CourseOnline.Application.Common.Interfaces;

public interface IRepositoryBase<TModel, TId>
{
    Task<TModel> AddASync(TModel model, CancellationToken ct);
    Task<TModel?> GetByIdAsync(TId id, CancellationToken ct);
    Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct);
    Task<TModel?> UpdateAsync(TId id, TModel model, CancellationToken ct);
    Task<bool> RemoveAsync(TId id, CancellationToken ct);
}

