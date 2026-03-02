using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.StudentCourses;
public interface IStudentCourseService
{
    Task<Result> AddStudentToCourseAsync(Guid studentId, Guid courseId, CancellationToken ct);
    Task<Result> RemoveStudentFromCourseAsync(Guid studentId, Guid courseId, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Course>>> GetCoursesByStudentIdAsync(Guid studentId, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Student>>> GetStudentsByCourseIdAsync(Guid courseId, CancellationToken ct);

}
