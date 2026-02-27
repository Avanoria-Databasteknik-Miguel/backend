using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.Teachers;

public interface ITeacherService
{
    Task<Result<Teacher>> CreateTeacherAsync(CreateTeacherInput input, CancellationToken ct);
    Task<Result<Teacher>> GetTeacherByIdAsync(Guid id, CancellationToken ct);
    Task<Result<Teacher>> GetTeacherByEmail(string email, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Teacher>>> GetTeachersAsync(CancellationToken ct);
    Task<Result<Teacher>> UpdateTeacherAsync(UpdateTeacherInput input, CancellationToken ct);
    Task<Result> DeleteTeacherAsync(Guid id, CancellationToken ct);
}