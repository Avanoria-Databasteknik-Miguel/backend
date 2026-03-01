using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Floors.Interfaces;
public interface IFloorRepository : IRepositoryBase<Floor, int>
{
    Task<Floor?> GetByLevelAsync(string level, CancellationToken ct);
}
