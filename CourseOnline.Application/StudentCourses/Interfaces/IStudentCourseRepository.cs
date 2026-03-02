using CourseOnline.Domain.Models;

namespace CourseOnline.Application.StudentCourses.Interfaces;
public interface IStudentCourseRepository
{
    Task<bool> ExistsAsync(Guid studentId, Guid courseId, CancellationToken ct);
    Task AddAsync(Guid studentId, Guid courseId, CancellationToken ct);
    Task<bool> RemoveAsync(Guid studentId, Guid courseId, CancellationToken ct);
    Task<IReadOnlyCollection<Course>> GetCoursesByStudentIdAsync(Guid studentId, CancellationToken ct);
    Task<IReadOnlyCollection<Student>> GetStudentsByCourseIdAsync(Guid courseId, CancellationToken ct);
}
