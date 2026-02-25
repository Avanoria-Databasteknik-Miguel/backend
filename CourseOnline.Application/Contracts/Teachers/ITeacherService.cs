using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.Teachers;
public interface ITeacherService
{
    Task<Teacher?> CreateTeacherAsync(CreateTeacherInput input, CancellationToken ct);
    Task<Teacher?> GetTeacherByIdAsync(Guid id, CancellationToken ct);
    Task<Teacher?> GetTeacherByEmail(string email, CancellationToken ct);
    Task<IReadOnlyCollection<Teacher>> GetTeachersAsync(CancellationToken ct);
    Task<Teacher?> UpdateTeacher(UpdateTeacherInput input, CancellationToken ct);
    Task<bool> DeleteTeacherAsync(Guid id, CancellationToken ct);
}
