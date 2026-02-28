using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Courses.DTOs.Inputs;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.Courses;
public interface ICourseService
{
    Task<Result<Course>> CreateCourseAsync(CreateCourseInput input, CancellationToken ct);
    Task<Result<Course>> GetCourseByIdAsync(Guid id, CancellationToken ct);
    Task<Result<Course>> GetCourseByNameAsync(string name, CancellationToken ct);
    Task<Result<Course>> UpdateCourseAsync(UpdateCourseInput input, CancellationToken ct);
    Task<Result> DeleteCourseAsync(DeleteCourseInput id, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Course>>> GetAllCoursesAsync(CancellationToken ct);
}
