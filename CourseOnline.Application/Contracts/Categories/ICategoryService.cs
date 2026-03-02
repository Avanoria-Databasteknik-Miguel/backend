using CourseOnline.Application.Categories.DTOs;
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.Categories;
public interface ICategoryService
{
    Task<Result<Category>> CreateCategoryAsync(CreateCategoryInput input, CancellationToken ct);
    Task<Result<Category>> UpdateCategoryAsync(UpdateCategoryInput input, CancellationToken ct);
    Task<Result> DeleteCategoryAsync(DeleteCategoryInput input, CancellationToken ct);
    Task<Result<Category>> GetCategoryById(Guid id, CancellationToken ct);
    Task<IReadOnlyCollection<Category>> GetAllCategories(CancellationToken ct);
}
