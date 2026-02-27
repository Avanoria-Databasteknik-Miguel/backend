using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Programs.Interfaces;
public interface IProgramRepository : IRepositoryBase<Program, Guid>
{
    Task<Program?> GetByNameAsync(string name, CancellationToken ct);
}
