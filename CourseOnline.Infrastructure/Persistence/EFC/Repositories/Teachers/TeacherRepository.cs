using CourseOnline.Application.Teachers.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Teachers;

public class TeacherRepository(CourseOnlineDbContext context) : RepositoryBase<Teacher, Guid, TeacherEntity, CourseOnlineDbContext>(context), ITeacherRepository
{

    protected override TeacherEntity ToEntity(Teacher model)
    {
        return new TeacherEntity()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            ImageUrl = model.ImageUrl,
            
        };
    }

    protected override Teacher ToModel(TeacherEntity entity)
    {
        return new Teacher(entity.Id, entity.FirstName, entity.LastName, entity.Email, entity.ImageUrl);
    }



    public async Task<Teacher?> GetByEmailAsync(string email, CancellationToken ct)
    {

        var entity = await Context.Teachers.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email, ct);
        return entity is null ? default : ToModel(entity);

    }


}
