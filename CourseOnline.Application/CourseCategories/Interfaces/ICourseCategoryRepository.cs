using CourseOnline.Domain.Models;

namespace CourseOnline.Application.CourseCategories.Interfaces;
public interface ICourseCategoryRepository
{
    Task<bool> ExistsAsync(Guid courseId, Guid categoryId, CancellationToken ct);

    Task AddAsync(Guid courseId, Guid categoryId, CancellationToken ct);

    Task<bool> RemoveAsync(Guid courseId, Guid categoryId, CancellationToken ct);

    Task<IReadOnlyCollection<Category>> GetCategoriesByCourseIdAsync(Guid courseId, CancellationToken ct);

    Task<IReadOnlyCollection<Course>> GetCoursesByCategoryIdAsync(Guid categoryId, CancellationToken ct);
}
