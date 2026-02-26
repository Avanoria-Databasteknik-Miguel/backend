using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Programs.Interfaces;
public interface IProgramRepository : IRepositoryBase<Program, Guid>
{
    Task<Result<Program>> GetByNameAsync(string name, CancellationToken ct);
}
