using CourseOnline.Application.Contracts.Programs;
using CourseOnline.Application.Contracts.Teachers;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Extensions;
using CourseOnline.Infrastructure.Persistence;
using CourseOnline.Presentation.API.Models.Programs;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddCors();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());



//      ##### TEACHERS #####

app.MapPost("/api/teachers", async (
    CreateTeacherInput input,
    ITeacherService service,
    CancellationToken ct) =>
{
    var teacher = await service.CreateTeacherAsync(input, ct);

    if (teacher is null)
        return Results.BadRequest();

    return Results.Created($"/api/teachers/{teacher.Id}", teacher);
});


app.MapGet("/api/teachers", async (ITeacherService service, CancellationToken ct) =>
{
    var teachers = await service.GetTeachersAsync(ct);

    if (teachers is null)
        return Results.BadRequest();

    return Results.Ok(teachers);
});

app.MapPut("/api/teachers", async (UpdateTeacherInput input, ITeacherService service, CancellationToken ct) =>
{

    var teacher = await service.UpdateTeacherAsync(input, ct);
    return teacher is null ? Results.NotFound() : Results.Ok(teacher);

});

app.MapDelete("/api/teachers/{id:Guid}", async (Guid id, ITeacherService service, CancellationToken ct) =>
{
    var deleted = await service.DeleteTeacherAsync(id, ct);

    return Results.Ok(deleted);
});



//      ##### PROGRAMS #####

app.MapPost("/api/programs", async (CreateProgramRequest req, IProgramService service, CancellationToken ct) =>
{

    var ToInput = new CreateProgramInput(req.Name, req.DurationWeeks, req.MaxStudent);

    
    var program = await service.CreateProgramAsync(ToInput, ct);

    if (!program.Success) Results.BadRequest(program.ErrorMessage);

    

    return Results.Created($"/api/programs/{program.Value.Id}", program);

});

app.MapGet("/api/programs", async (IProgramService service, CancellationToken ct) =>
{
    var programs = await service.GetAllProgramsAsync(ct);

    return Results.Ok(programs.Value);
});


app.MapPut("/api/programs/{id:Guid}", async (Guid id, IProgramService service, CancellationToken ct) =>
{
    var program = await service.GetProgramByIdAsync(id, ct);

    if (!program.Success) Results.NotFound(program.ErrorMessage);

    return Results.Ok(program.Value);
});


app.MapDelete("/api/programs/{id:Guid}", async (Guid id, IProgramService service, CancellationToken ct) =>
{
    var program = await service.GetProgramByIdAsync(id, ct);

    if (!program.Success) return Results.BadRequest(program.ErrorMessage);

    var programId = new DeleteProgramInput(program.Value.Id);

    var deleted = await service.DeleteProgramAsync(programId, ct);

    return Results.Ok();

});


app.Run();
