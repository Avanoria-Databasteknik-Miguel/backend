using CourseOnline.Application.Classrooms.DTOs;
using CourseOnline.Application.Classrooms.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.Classrooms;
using CourseOnline.Application.Floors.Interfaces;
using CourseOnline.Domain.Models;

namespace CourseOnline.Application.Services;

public sealed class ClassroomService(IClassroomsRepository classroomRepo, IFloorRepository floorRepo) : IClassroomService
{
    public async Task<Result<Classroom>> CreateClassroomAsync(CreateClassroomInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.Name))
            return Result<Classroom>.BadRequest("Classroom name is required.");

        if (input.Seats <= 0)
            return Result<Classroom>.BadRequest("Seats must be greater than 0.");

        if (input.FloorId <= 0)
            return Result<Classroom>.BadRequest("FloorId is required.");

        var floor = await floorRepo.GetByIdAsync(input.FloorId, ct);
        if (floor is null)
            return Result<Classroom>.NotFound("Floor not found.");

        // OBS: kräver att Domain tillåter id=0 vid create
        var classroom = new Classroom(
            id: 0,
            name: input.Name,
            seats: input.Seats,
            floorId: input.FloorId
        );

        var created = await classroomRepo.AddASync(classroom, ct);
        return Result<Classroom>.Ok(created);
    }

    public async Task<Result<Classroom>> UpdateClassroomAsync(UpdateClassroomInput input, CancellationToken ct)
    {
        if (input.Id <= 0)
            return Result<Classroom>.BadRequest("Id is required.");

        if (string.IsNullOrWhiteSpace(input.Name))
            return Result<Classroom>.BadRequest("Classroom name is required.");

        if (input.Seats <= 0)
            return Result<Classroom>.BadRequest("Seats must be greater than 0.");

        if (input.FloorId <= 0)
            return Result<Classroom>.BadRequest("FloorId is required.");

        var existing = await classroomRepo.GetByIdAsync(input.Id, ct);
        if (existing is null)
            return Result<Classroom>.NotFound("Classroom not found.");

        var floor = await floorRepo.GetByIdAsync(input.FloorId, ct);
        if (floor is null)
            return Result<Classroom>.NotFound("Floor not found.");

        existing.Update(input.Name, input.Seats, input.FloorId);

        var updated = await classroomRepo.UpdateAsync(input.Id, existing, ct);

        return updated is null
            ? Result<Classroom>.Conflict("Something went wrong.")
            : Result<Classroom>.Ok(updated);
    }

    public async Task<Result> DeleteClassroomAsync(DeleteClassroomInput input, CancellationToken ct)
    {
        if (input.Id <= 0)
            return Result.BadRequest("Id is required.");

        var existing = await classroomRepo.GetByIdAsync(input.Id, ct);
        if (existing is null)
            return Result.NotFound("Classroom not found.");

        var deleted = await classroomRepo.RemoveAsync(existing.Id, ct);

        return deleted
            ? Result.Ok()
            : Result.Conflict("Something went wrong.");
    }

    public async Task<Result<Classroom>> GetClassroomByIdAsync(int id, CancellationToken ct)
    {
        if (id <= 0)
            return Result<Classroom>.BadRequest("Id is required.");

        var classroom = await classroomRepo.GetByIdAsync(id, ct);

        return classroom is null
            ? Result<Classroom>.NotFound("Classroom not found.")
            : Result<Classroom>.Ok(classroom);
    }

    public async Task<Result<IReadOnlyCollection<Classroom>>> GetAllClassroomsAsync(CancellationToken ct)
    {
        var classrooms = await classroomRepo.GetAllAsync(ct);
        return Result<IReadOnlyCollection<Classroom>>.Ok(classrooms);
    }
}
