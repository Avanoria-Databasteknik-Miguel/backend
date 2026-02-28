using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Students.Interfaces;
public interface IStudentRepository : IRepositoryBase<Student, Guid>
{
    Task<Result<Program>> GetByNameAsync(string email, CancellationToken ct);
}
