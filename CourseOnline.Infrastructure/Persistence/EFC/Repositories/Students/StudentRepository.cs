using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Students.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Students;

public class StudentRepository(CourseOnlineDbContext context) : RepositoryBase<Student, Guid, StudentEntity, CourseOnlineDbContext>(context), IStudentRepository
{
    public async Task<Student?> GetByEmailAsync(string email, CancellationToken ct)
    {
        var entity = await Context.Students.AsNoTracking().SingleOrDefaultAsync(s => s.Email == email, ct);

        return entity is null ? default : ToModel(entity);
    }

    protected override StudentEntity ToEntity(Student model)
    {
        return new StudentEntity()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            ImageUrl = model.ImageUrl,
            ProgramId = model.ProgramId
        };
    }
    protected override Student ToModel(StudentEntity entity)
    {
        return new Student(entity.Id, entity.FirstName, entity.LastName, entity.Email, entity.ImageUrl, entity.ProgramId);
    }


}