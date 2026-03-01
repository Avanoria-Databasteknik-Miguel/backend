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
    public async Task<Result<IReadOnlyCollection<CourseSession>>> GetByCourseIdAsync(Guid courseId, CancellationToken ct)
    {
        if (courseId == Guid.Empty) return Result<IReadOnlyCollection<CourseSession>>.BadRequest("Course Id is required");

        var entities = await Context.CourseSessions.AsNoTracking().Where(x => x.CourseId == courseId).ToListAsync(ct);

        return entities.Count == 0 ? Result<IReadOnlyCollection<CourseSession>>.NotFound("No sessions found for this course.") : Result<IReadOnlyCollection<CourseSession>>.Ok([.. entities.Select(ToModel)]);
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
