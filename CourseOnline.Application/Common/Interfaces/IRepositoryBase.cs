namespace CourseOnline.Application.Common.Interfaces;

public interface IRepositoryBase<TModel, in TId>
{
    Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default);
    Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default);
    Task<TModel> AddASync(TModel model, CancellationToken ct = default);
    Task<TModel?> UpdateAsync(TId id, TModel model, CancellationToken ct = default);
    Task<bool> RemoveAsync(TId id, CancellationToken ct = default);
}

