using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Programs;
public class ProgramRepository(CourseOnlineDbContext context) : RepositoryBase<Program, Guid, ProgramEntity, CourseOnlineDbContext>
{
}
