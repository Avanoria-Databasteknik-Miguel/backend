using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Infrastructure.Persistence.Contexts;

namespace CourseOnline.Infrastructure.Persistence.EFC.UnitOfWork;

public class EfcUnitOfWork(CourseOnlineDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => context.SaveChangesAsync(ct);
}
