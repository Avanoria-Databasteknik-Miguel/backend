using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Programs.DTOs.Outputs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Programs.Interfaces;
public interface IProgramRepository : IRepositoryBase<Program, Guid>
{
    Task<Program?> GetByNameAsync(string name, CancellationToken ct);
    Task<ProgramOutput?> GetOutputByIdAsync(Guid id, CancellationToken ct);
}
