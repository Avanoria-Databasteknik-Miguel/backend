using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Domain.Models;
using System.Net.Http.Headers;

namespace CourseOnline.Application.Contracts.Programs;
public interface IProgramService
{
    Task<Result<Program>> CreateProgramAsync(CreateProgramInput input, CancellationToken ct);
    Task<Result<Program>> GetProgramByIdAsync(Guid id, CancellationToken ct);
    Task<Result<Program>> GetProgramByNameAsync(string name, CancellationToken ct);
    Task<Result<Program>> UpdateProgramAsync(UpdateProgramInput input, CancellationToken ct);
    Task<Result> DeleteProgramAsync(DeleteProgramInput id, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Program>>> GetAllProgramsAsync(CancellationToken ct);
}
