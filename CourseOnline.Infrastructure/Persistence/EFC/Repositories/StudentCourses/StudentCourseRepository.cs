using CourseOnline.Application.StudentCourses.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.StudentCourses;

public class StudentCourseRepository(CourseOnlineDbContext context) : IStudentCourseRepository
{
    private readonly CourseOnlineDbContext _context = context;
    public async Task AddAsync(Guid studentId, Guid courseId, CancellationToken ct)
    {
        if (await ExistsAsync(studentId, courseId, ct)) return;

        var entity = new StudentCourseEntity
        {
            StudentId = studentId,
            CourseId = courseId
        };

        await _context.StudentCourses.AddAsync(entity, ct);
    }

    public async Task<bool> ExistsAsync(Guid studentId, Guid courseId, CancellationToken ct)
    {
        return await _context.StudentCourses.AsNoTracking().AnyAsync(x => x.StudentId == studentId && x.CourseId == courseId, ct);
    }

    public async Task<IReadOnlyCollection<Course>> GetCoursesByStudentIdAsync(Guid studentId, CancellationToken ct)
    {
        var courses = await _context.StudentCourses
            .AsNoTracking()
            .Where(x => x.StudentId == studentId)
            .Select(x => x.Course)
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

    public async Task<IReadOnlyCollection<Student>> GetStudentsByCourseIdAsync(Guid courseId, CancellationToken ct)
    {
        var students = await _context.StudentCourses.AsNoTracking().Where(x => x.CourseId == courseId).Select(x => x.Student).ToListAsync(ct);


        return [.. students.Select(s => new Student(id: s.Id, firstName: s.FirstName, lastName: s.LastName, email: s.Email, imageUrl: s.ImageUrl, programId: s.ProgramId))];
    }

    public async Task<bool> RemoveAsync(Guid studentId, Guid courseId, CancellationToken ct)
    {
        var entity = await _context.StudentCourses.SingleOrDefaultAsync(x => x.StudentId == studentId && x.CourseId == courseId, ct);

        if (entity is null) return false;

        _context.StudentCourses.Remove(entity);
  
        return true;
    }
}
