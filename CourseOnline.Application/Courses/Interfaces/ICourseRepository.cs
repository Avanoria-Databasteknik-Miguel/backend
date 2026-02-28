using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Courses.Interfaces;
public interface ICourseRepository : IRepositoryBase<Course, Guid>
{
    Task<Course?> GetByNameAsync(string name, CancellationToken ct);
}
