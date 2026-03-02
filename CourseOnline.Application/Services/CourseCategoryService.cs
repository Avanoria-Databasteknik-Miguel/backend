using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.CourseCategories;
using CourseOnline.Application.CourseCategories.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public class CourseCategoryService(ICourseCategoryRepository courseCategoryRepo, IUnitOfWork uow) : ICourseCategoryService
{
    public async Task<Result> AddCategoryToCourseAsync(Guid courseId, Guid categoryId, CancellationToken ct)
    {

        if (courseId == Guid.Empty)
            return Result.BadRequest("CourseId is required.");

        if (categoryId == Guid.Empty)
            return Result.BadRequest("CategoryId is required.");

        if (await courseCategoryRepo.ExistsAsync(courseId, categoryId, ct))
            return Result.Conflict("Category already assigned to course.");

        await courseCategoryRepo.AddAsync(courseId, categoryId, ct);
        await uow.SaveChangesAsync(ct);

        return Result.Ok();
    }

    public async Task<Result<IReadOnlyCollection<Category>>> GetCategoriesByCourseIdAsync(Guid courseId, CancellationToken ct)
    {
        if (courseId == Guid.Empty)
            return Result<IReadOnlyCollection<Category>>.BadRequest("CourseId is required.");

        var categories = await courseCategoryRepo.GetCategoriesByCourseIdAsync(courseId, ct);

        return Result<IReadOnlyCollection<Category>>.Ok(categories);
    }

    public async Task<Result<IReadOnlyCollection<Course>>> GetCoursesByCategoryIdAsync(Guid categoryId, CancellationToken ct)
    {
        if (categoryId == Guid.Empty)
            return Result<IReadOnlyCollection<Course>>.BadRequest("CategoryId is required.");

        var courses = await courseCategoryRepo.GetCoursesByCategoryIdAsync(categoryId, ct);

        return Result<IReadOnlyCollection<Course>>.Ok(courses);
    }

    public async Task<Result> RemoveCategoryFromCourseAsync(Guid courseId, Guid categoryId, CancellationToken ct)
    {
        if (courseId == Guid.Empty)
            return Result.BadRequest("CourseId is required.");

        if (categoryId == Guid.Empty)
            return Result.BadRequest("CategoryId is required.");

        var removed = await courseCategoryRepo.RemoveAsync(courseId, categoryId, ct);
        if (!removed)
            return Result.NotFound("CourseCategory relation not found.");

        await uow.SaveChangesAsync(ct);

        return Result.Ok();
    }
}
