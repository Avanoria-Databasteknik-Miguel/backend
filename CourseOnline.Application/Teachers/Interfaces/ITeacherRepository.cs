using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Teachers.Interfaces;
public interface ITeacherRepository : IRepositoryBase<Teacher, string>
{
}
