using CourseOnline.Application.Categories.DTOs.Inputs;
using CourseOnline.Application.Categories.Interfaces;
using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.Categories;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public class CategoryService(ICategoryRepository categoryRepo, IUnitOfWork uow) : ICategoryService
{
    public async Task<Result<Category>> CreateCategoryAsync(CreateCategoryInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.Name))
            return Result<Category>.BadRequest("Name is required.");

        var normalizedName = input.Name.Trim().ToLower();

        if (await categoryRepo.ExistsByNameAsync(normalizedName, ct))
            return Result<Category>.Conflict("Category already exists.");

        var category = new Category(Guid.NewGuid(), normalizedName);

        await categoryRepo.AddASync(category, ct);
        await uow.SaveChangesAsync(ct);

        return Result<Category>.Ok(category);
    }

    public async Task<Result> DeleteCategoryAsync(DeleteCategoryInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty)
            return Result.BadRequest("Id is required.");

        var existing = await categoryRepo.GetByIdAsync(input.Id, ct);
        if (existing is null)
            return Result.NotFound("Category not found.");

        var deleted = await categoryRepo.RemoveAsync(existing.Id, ct);
        if (!deleted) return Result.Conflict("Something went wrong");

        await uow.SaveChangesAsync(ct);

        return Result.Ok();

    }

    public async Task<Result<IReadOnlyCollection<Category>>> GetAllCategoriesAsync(CancellationToken ct)
    {
        var categories = await categoryRepo.GetAllAsync(ct);
        return Result<IReadOnlyCollection<Category>>.Ok(categories);
    }

    public async Task<Result<Category>> GetCategoryByIdAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty)
            return Result<Category>.BadRequest("Id is required.");

        var category = await categoryRepo.GetByIdAsync(id, ct);

        return category is null
            ? Result<Category>.NotFound("Category not found.")
            : Result<Category>.Ok(category);
    }

    public async Task<Result<Category>> UpdateCategoryAsync(UpdateCategoryInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty)
            return Result<Category>.BadRequest("Id is required.");

        if (string.IsNullOrWhiteSpace(input.Name))
            return Result<Category>.BadRequest("Name is required.");

        var existing = await categoryRepo.GetByIdAsync(input.Id, ct);
        if (existing is null)
            return Result<Category>.NotFound("Category not found.");

        var normalizedName = input.Name.Trim().ToLower();

        var existingWithSameName = await categoryRepo.GetByNameAsync(normalizedName, ct);
        if (existingWithSameName is not null && existingWithSameName.Id != input.Id)
            return Result<Category>.Conflict("Category name already exists.");

        existing.Update(normalizedName);

        var updated = await categoryRepo.UpdateAsync(input.Id, existing, ct);
        if (updated is null) return Result<Category>.Conflict("Something went wrong.");

        await uow.SaveChangesAsync(ct);

        return Result<Category>.Ok(updated);
    }
}
