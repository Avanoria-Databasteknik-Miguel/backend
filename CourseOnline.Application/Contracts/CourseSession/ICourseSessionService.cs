using CourseOnline.Application.Common.Results;
using CourseOnline.Application.CourseSessions.DTOs.Inputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.CourseSessions;
public interface ICourseSessionService
{
    Task<Result<CourseSession>> CreateCourseSessionAsync(CreateCourseSessionInput input, CancellationToken ct);
    Task<Result<CourseSession>> GetCourseSessionByIdAsync(Guid Id, CancellationToken ct);
    Task<Result<IReadOnlyCollection<CourseSession>>> GetAllCourseSessionsAsync(CancellationToken ct);
    Task<Result<CourseSession>> UpdateCourseSessionAsync(UpdateCourseSessionInput input, CancellationToken ct);
    Task<Result> DeleteCourseSessionAsync(DeleteCourseSessionInput input, CancellationToken ct);
}
