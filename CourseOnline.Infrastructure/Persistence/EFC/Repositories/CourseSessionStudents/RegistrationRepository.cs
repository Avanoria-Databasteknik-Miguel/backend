using CourseOnline.Application.Registrations.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.CourseSessionStudents;

public class RegistrationRepository(CourseOnlineDbContext context): IRegistrationRepository
{
    private readonly CourseOnlineDbContext _context = context;

    public async Task<CourseSessionStudent> AddAsync(CourseSessionStudent registration, CancellationToken ct)
    {
        var entity = new CourseSessionStudentEntity
        {
            StudentId = registration.StudentId,
            CourseSessionId = registration.CourseSessionId
        };

        await _context.CourseSessionStudents.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);

        return registration;
    }

    public async Task<int> CountBySessionIdAsync(Guid courseSessionId, CancellationToken ct)
    {
        return await _context.CourseSessionStudents
            .AsNoTracking()
            .CountAsync(x => x.CourseSessionId == courseSessionId, ct);
    }

    public async Task<bool> ExistsAsync(Guid studentId, Guid courseSessionId, CancellationToken ct)
    {
        return await _context.CourseSessionStudents
            .AsNoTracking()
            .AnyAsync(x => x.StudentId == studentId && x.CourseSessionId == courseSessionId, ct);
    }

    public async Task<IReadOnlyCollection<CourseSessionStudent>> GetBySessionIdAsync(Guid courseSessionId, CancellationToken ct)
    {
        var entities = await _context.CourseSessionStudents
            .AsNoTracking()
            .Where(x => x.CourseSessionId == courseSessionId)
            .ToListAsync(ct);

        return entities
            .Select(x => new CourseSessionStudent(x.StudentId, x.CourseSessionId))
            .ToList();
    }

    public async Task<IReadOnlyCollection<CourseSessionStudent>> GetByStudentIdAsync(Guid studentId, CancellationToken ct)
    {
        var entities = await _context.CourseSessionStudents
            .AsNoTracking()
            .Where(x => x.StudentId == studentId)
            .ToListAsync(ct);

        return entities
            .Select(x => new CourseSessionStudent(x.StudentId, x.CourseSessionId))
            .ToList();
    }

    public async Task<bool> RemoveAsync(Guid studentId, Guid courseSessionId, CancellationToken ct)
    {
        var entity = await _context.CourseSessionStudents
            .SingleOrDefaultAsync(x => x.StudentId == studentId && x.CourseSessionId == courseSessionId, ct);

        if (entity is null)
            return false;

        _context.CourseSessionStudents.Remove(entity);
        await _context.SaveChangesAsync(ct);

        return true;
    }
}
