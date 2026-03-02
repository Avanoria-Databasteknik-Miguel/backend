using CourseOnline.Application.Categories.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Persistence.EFC.Repositories.Categories;

public sealed class CategoryRepository(CourseOnlineDbContext context) : RepositoryBase<Category, Guid, CategoryEntity, CourseOnlineDbContext>(context), ICategoryRepository
{
    public async Task<Category?> GetByNameAsync(string name, CancellationToken ct)
    {
        var entity = await Context.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.Name == name, ct);
        return entity is null ? null : ToModel(entity);
    }

    protected override CategoryEntity ToEntity(Category model)
    {
        return new CategoryEntity()
        {
            Id = model.Id,
            Name = model.Name
        };
    }

    protected override Category ToModel(CategoryEntity entity)
    {
        return new Category(entity.Id, entity.Name);

    }
}
