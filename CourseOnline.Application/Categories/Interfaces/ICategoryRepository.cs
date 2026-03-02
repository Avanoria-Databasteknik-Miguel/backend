using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Categories.Interfaces;
public interface ICategoryRepository : IRepositoryBase<Category, Guid>
{
    Task<Result<Category>> GetCategoryByName(string name, CancellationToken ct);
}
