using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Categories.Interfaces;
public interface ICategoryRepository : IRepositoryBase<Category, Guid>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken ct);
}
