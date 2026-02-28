using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Students.Interfaces;
public interface IStudentRepository : IRepositoryBase<Student, Guid>
{
}
