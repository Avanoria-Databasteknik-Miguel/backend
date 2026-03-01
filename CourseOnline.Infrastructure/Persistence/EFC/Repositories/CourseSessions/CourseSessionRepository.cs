using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Application.CourseSessions.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;


namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.CourseSessions;

public class CourseSessionRepository(CourseOnlineDbContext context) : RepositoryBase<CourseSession, Guid, CourseSessionEntity, CourseOnlineDbContext>(context), ICourseSessionRepository
{
    public async Task<Result<CourseSession>> GetByCourseIdAsync(Guid courseId, CancellationToken ct)
    {
        if (courseId == Guid.Empty) return Result<CourseSession>.BadRequest("Course Id is required");

        var entity = await Context.CourseSessions.AsNoTracking().SingleOrDefaultAsync(x => x.CourseId == courseId, ct);

        return entity is null ? Result<CourseSession>.BadRequest("Course session not found") : Result<CourseSession>.Ok(ToModel(entity));
    }

    protected override CourseSessionEntity ToEntity(CourseSession model) => new()
    {
        Id = model.Id,
        CourseId = model.CourseId,
        ClassroomId = model.ClassroomId,
        StartDateTimeUtc = model.StartDateTimeUtc,
        EndDateTimeUtc = model.EndDateTimeUtc
    };

    protected override CourseSession ToModel(CourseSessionEntity entity) => new(entity.Id, entity.CourseId, entity.ClassroomId, entity.StartDateTimeUtc, entity.EndDateTimeUtc);

}
