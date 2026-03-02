using CourseOnline.Application.Common.Interfaces;
using CourseOnline.Application.Common.Results;
using CourseOnline.Application.Contracts.Programs;
using CourseOnline.Application.Factories;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Application.Programs.DTOs.Outputs;
using CourseOnline.Application.Programs.Interfaces;
using CourseOnline.Domain.Models;


namespace CourseOnline.Application.Services;

public sealed class ProgramService(IProgramRepository programRepo, IUnitOfWork uow) : IProgramService
{
    public async Task<Result<ProgramOutput>> CreateProgramAsync(CreateProgramInput input, CancellationToken ct)
    {
        if(string.IsNullOrWhiteSpace(input.Name)) return Result<ProgramOutput>.BadRequest("Program name is required.");
        if (input.DurationWeeks <= 0) return Result<ProgramOutput>.BadRequest("Duration weeks must be higher than zero");
        if (input.MaxStudents <= 0) return Result<ProgramOutput>.BadRequest("Limit of students must be higher than zero");

        var existingProgram = await programRepo.GetByNameAsync(input.Name, ct);

        if (existingProgram is not null) return Result<ProgramOutput>.Conflict("Program already exists");

        var program = ProgramFactory.Create(input);

        var createdProgram = await programRepo.AddASync(program, ct);

        await uow.SaveChangesAsync(ct);

        var output = await programRepo.GetOutputByIdAsync(program.Id, ct);
        if (output is null)
            return Result<ProgramOutput>.Conflict("Something went wrong.");


        //TODO: cache stuff

        return Result<ProgramOutput>.Ok(output);
    }

    public async Task<Result> DeleteProgramAsync(DeleteProgramInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty) return Result.BadRequest("Id is required.");

        var deleteProgram = await programRepo.GetByIdAsync(input.Id, ct);

        if (deleteProgram is null) return Result.NotFound("Program not found");

        var deleted = await programRepo.RemoveAsync(deleteProgram.Id, ct);

        if (!deleted) return Result.Conflict("Something went wrong");

        await uow.SaveChangesAsync(ct);

        return Result.Ok();

    }
    public async Task<Result<IReadOnlyCollection<Program>>> GetAllProgramsAsync(CancellationToken ct)
    {
        var programs = await programRepo.GetAllAsync(ct);

        return programs.Any() ? Result<IReadOnlyCollection<Program>>.Ok(programs) : Result<IReadOnlyCollection<Program>>.Ok([]);
    }

    public async Task<Result<Program>> GetProgramByIdAsync(Guid id, CancellationToken ct)
    {
        if (id == Guid.Empty) return Result<Program>.BadRequest("Email is required.");

        var program = await programRepo.GetByIdAsync(id, ct);

        return program is null ? Result<Program>.NotFound("Program not found") : Result<Program>.Ok(program);
    }

    public async Task<Result<Program>> GetProgramByNameAsync(string name, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(name)) return Result<Program>.BadRequest("Name is required.");

        var program = await programRepo.GetByNameAsync(name, ct);

        return program is null ? Result<Program>.NotFound("Program not found") : Result<Program>.Ok(program);
    }

    public async Task<Result<ProgramOutput>> UpdateProgramAsync(UpdateProgramInput input, CancellationToken ct)
    {
        if (input.Id == Guid.Empty) return Result<ProgramOutput>.BadRequest("Id required");

        var programToUpdate = await programRepo.GetByIdAsync(input.Id, ct);
        if (programToUpdate is null) return Result<ProgramOutput>.NotFound("Education program not found.");

        programToUpdate.Update(input.Name, input.DurationWeeks, input.MaxStudents);

        var updated = await programRepo.UpdateAsync(input.Id, programToUpdate, ct);
        if (updated is null)
            return Result<ProgramOutput>.Conflict("Something went wrong");

        await uow.SaveChangesAsync(ct);

        var output = await programRepo.GetOutputByIdAsync(input.Id, ct);
        if (output is null)
            return Result<ProgramOutput>.Conflict("Something went wrong");

        return Result<ProgramOutput>.Ok(output);
    }
}
