using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.Floors;
using CourseOnline.Application.Floors.DTOs;
using CourseOnline.Application.Floors.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public sealed class FloorService(IFloorRepository floorRepo) : IFloorService
{
    public async Task<Result<Floor>> CreateFloorAsync(CreateFloorInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.Level))
            return Result<Floor>.BadRequest("Level is required.");

        var normalized = input.Level.Trim().ToLower();

        var existing = await floorRepo.GetByLevelAsync(normalized, ct);
        if (existing is not null)
            return Result<Floor>.Conflict("Floor level already exists.");

        var floor = new Floor(id: 0, level: normalized);

        var created = await floorRepo.AddASync(floor, ct);

        return Result<Floor>.Ok(created);
    }

    public async Task<Result<Floor>> GetFloorByIdAsync(int id, CancellationToken ct)
    {
        if (id <= 0)
            return Result<Floor>.BadRequest("Id is required.");

        var floor = await floorRepo.GetByIdAsync(id, ct);

        return floor is null
            ? Result<Floor>.NotFound("Floor not found.")
            : Result<Floor>.Ok(floor);
    }

    public async Task<Result<Floor>> GetFloorByLevelAsync(string level, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(level))
            return Result<Floor>.BadRequest("Level is required.");

        var normalized = level.Trim().ToLower();

        var floor = await floorRepo.GetByLevelAsync(normalized, ct);

        return floor is null
            ? Result<Floor>.NotFound("Floor not found.")
            : Result<Floor>.Ok(floor);
    }

    public async Task<Result<IReadOnlyCollection<Floor>>> GetAllFloorsAsync(CancellationToken ct)
    {
        var floors = await floorRepo.GetAllAsync(ct);
        return Result<IReadOnlyCollection<Floor>>.Ok(floors);
    }

    public async Task<Result<Floor>> UpdateFloorAsync(UpdateFloorInput input, CancellationToken ct)
    {
        if (input.Id <= 0)
            return Result<Floor>.BadRequest("Id is required.");

        if (string.IsNullOrWhiteSpace(input.Level))
            return Result<Floor>.BadRequest("Level is required.");

        var existing = await floorRepo.GetByIdAsync(input.Id, ct);
        if (existing is null)
            return Result<Floor>.NotFound("Floor not found.");

        var normalized = input.Level.Trim().ToLower();

        var existingWithSameLevel = await floorRepo.GetByLevelAsync(normalized, ct);
        if (existingWithSameLevel is not null && existingWithSameLevel.Id != input.Id)
            return Result<Floor>.Conflict("Floor level already exists.");

        existing.Update(normalized);

        var updated = await floorRepo.UpdateAsync(input.Id, existing, ct);

        return updated is null
            ? Result<Floor>.Conflict("Something went wrong.")
            : Result<Floor>.Ok(updated);
    }

    public async Task<Result> DeleteFloorAsync(int id, CancellationToken ct)
    {
        if (id <= 0)
            return Result.BadRequest("Id is required.");

        var existing = await floorRepo.GetByIdAsync(id, ct);
        if (existing is null)
            return Result.NotFound("Floor not found.");

        var deleted = await floorRepo.RemoveAsync(existing.Id, ct);

        return deleted
            ? Result.Ok()
            : Result.Conflict("Something went wrong.");
    }
}
