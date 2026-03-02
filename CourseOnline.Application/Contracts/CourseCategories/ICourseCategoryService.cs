using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.CourseCategories;
public interface ICourseCategoryService
{
    Task<Result> AddCategoryToCourseAsync(Guid courseId, Guid categoryId, CancellationToken ct);
    Task<Result> RemoveCategoryFromCourseAsync(Guid courseId, Guid categoryId, CancellationToken ct);

    Task<Result<IReadOnlyCollection<Category>>> GetCategoriesByCourseIdAsync(Guid courseId, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Course>>> GetCoursesByCategoryIdAsync(Guid categoryId, CancellationToken ct);
}
