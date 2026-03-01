using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Floors.DTOs;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Contracts.Floors;
public interface IFloorService
{
    Task<Result<Floor>> CreateFloorAsync(CreateFloorInput input, CancellationToken ct);
    Task<Result<Floor>> GetFloorByIdAsync(int id, CancellationToken ct);
    Task<Result<Floor>> GetFloorByLevelAsync(string level, CancellationToken ct);
    Task<Result<Floor>> UpdateFloorAsync(UpdateFloorInput id, CancellationToken ct);
    Task<Result> DeleteFloorAsync(int id, CancellationToken ct);
    Task<Result<IReadOnlyCollection<Floor>>> GetAllFloorsAsync(CancellationToken ct);
}
