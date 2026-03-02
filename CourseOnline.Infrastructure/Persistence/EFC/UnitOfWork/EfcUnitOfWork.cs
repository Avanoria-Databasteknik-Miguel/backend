using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Infrastructure.Common.Entities;
using CourseOnline.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.UnitOfWork;

public sealed class EfcUnitOfWork(CourseOnlineDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                // Sätt Created bara vid insert
                if (entry.Entity.CreatedAtUtc == default)
                    entry.Entity.CreatedAtUtc = now;

                // Oftast vill man även sätt Modified vid insert
                entry.Entity.ModifiedAtUtc = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                // Created ska aldrig ändras vid update
                entry.Property(x => x.CreatedAtUtc).IsModified = false;

                entry.Entity.ModifiedAtUtc = now;
            }
        }

        return await context.SaveChangesAsync(ct);
    }
}
