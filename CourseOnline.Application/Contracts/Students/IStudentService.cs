using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Students.DTOs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.Students;
public interface IStudentService
{
    Task<Result<Student>> CreateStudentAsync(CreateStudentInput input, CancellationToken ct);
    Task<Result<Student>> GetStudentByIdAsync(Guid id, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Student>>> GetStudentsAsync(CancellationToken ct);
    Task<Result<Student>> UpdateStudentAsync(UpdateStudentInput input, CancellationToken ct);
    Task<Result> DeleteStudentAsync(Guid id, CancellationToken ct);
}
