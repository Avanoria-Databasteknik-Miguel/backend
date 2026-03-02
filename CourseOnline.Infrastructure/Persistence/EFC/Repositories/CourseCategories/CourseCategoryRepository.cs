using CourseOnline.Application.CourseCategories.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.CourseCategories;

public sealed class CourseCategoryRepository(CourseOnlineDbContext context) : ICourseCategoryRepository
{
    private readonly CourseOnlineDbContext _context = context;

    public async Task<bool> ExistsAsync(Guid courseId, Guid categoryId, CancellationToken ct)
    {
        return await _context.CourseCategories
            .AsNoTracking()
            .AnyAsync(x => x.CourseId == courseId && x.CategoryId == categoryId, ct);
    }

    public async Task AddAsync(Guid courseId, Guid categoryId, CancellationToken ct)
    {
        // valfritt skydd: om den redan finns så gör inget (eller kasta)
        var exists = await ExistsAsync(courseId, categoryId, ct);
        if (exists) return;

        var entity = new CourseCategoryEntity
        {
            CourseId = courseId,
            CategoryId = categoryId
        };

        await _context.CourseCategories.AddAsync(entity, ct);
        // INGEN SaveChanges här (UoW i service)
    }

    public async Task<bool> RemoveAsync(Guid courseId, Guid categoryId, CancellationToken ct)
    {
        var entity = await _context.CourseCategories
            .SingleOrDefaultAsync(x => x.CourseId == courseId && x.CategoryId == categoryId, ct);

        if (entity is null) return false;

        _context.CourseCategories.Remove(entity);
        // INGEN SaveChanges här (UoW i service)
        return true;
    }

    public async Task<IReadOnlyCollection<Category>> GetCategoriesByCourseIdAsync(Guid courseId, CancellationToken ct)
    {
        // Hämta categories via join-tabellen
        var categories = await _context.CourseCategories
            .AsNoTracking()
            .Where(x => x.CourseId == courseId)
            .Select(x => x.Category) // kräver nav: CourseCategoryEntity.Category
            .ToListAsync(ct);

        // Mappa EF -> Domain
        return categories
            .Select(c => new Category(c.Id, c.Name))
            .ToList();
    }

    public async Task<IReadOnlyCollection<Course>> GetCoursesByCategoryIdAsync(Guid categoryId, CancellationToken ct)
    {
        // Hämta courses via join-tabellen
        var courses = await _context.CourseCategories
            .AsNoTracking()
            .Where(x => x.CategoryId == categoryId)
            .Select(x => x.Course) // kräver nav: CourseCategoryEntity.Course
            .ToListAsync(ct);


        return courses
            .Select(c => new Course(
                id: c.Id,
                name: c.Name,
                durationWeeks: c.DurationWeeks,
                maxStudents: c.MaxStudents,
                teacherId: c.TeacherId,
                programId: c.ProgramId
            ))
            .ToList();


    }
}
