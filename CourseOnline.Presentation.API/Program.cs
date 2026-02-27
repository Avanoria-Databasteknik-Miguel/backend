using CourseOnline.Application.Contracts.Programs;
using CourseOnline.Application.Contracts.Teachers;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Infrastructure.Extensions;
using CourseOnline.Presentation.API.Common;
using CourseOnline.Presentation.API.Models.Programs;


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

    if (teacher.Success)
        return Results.Created($"/api/teachers/{teacher.Value!.Id}", teacher.Value);

    return teacher.ToHttpResult();
});


app.MapGet("/api/teachers", async (ITeacherService service, CancellationToken ct) =>
{
    var teachers = await service.GetTeachersAsync(ct);


    return teachers.ToHttpResult();
});

app.MapPut("/api/teachers", async (UpdateTeacherInput input, ITeacherService service, CancellationToken ct) =>
{

    var teacher = await service.UpdateTeacherAsync(input, ct);
    return teacher.ToHttpResult();

});

app.MapDelete("/api/teachers/{id:Guid}", async (Guid id, ITeacherService service, CancellationToken ct) =>
{
    var deleted = await service.DeleteTeacherAsync(id, ct);

    return deleted.ToHttpResult();
});



//      ##### PROGRAMS #####

app.MapPost("/api/programs", async (
    CreateProgramRequest req,
    IProgramService service,
    CancellationToken ct) =>
{
    var ToInput = new CreateProgramInput(req.Name, req.DurationWeeks, req.MaxStudents);

    var program = await service.CreateProgramAsync(ToInput, ct);

    if (program.Success)
        return Results.Created($"/api/programs/{program.Value!.Id}", program.Value);

    return program.ToHttpResult();
});

app.MapGet("/api/programs", async (
    IProgramService service,
    CancellationToken ct) =>
{
    var programs = await service.GetAllProgramsAsync(ct);

    return programs.ToHttpResult();
});

app.MapPut("/api/programs/{id:Guid}", async (
    Guid id,
    UpdateProgramInput input,
    IProgramService service,
    CancellationToken ct) =>
{
    var cmd = input with { Id = id };

    var updated = await service.UpdateProgramAsync(cmd, ct);

    return updated.ToHttpResult();
});


app.MapDelete("/api/programs/{id:Guid}", async (
    Guid id,
    IProgramService service,
    CancellationToken ct) =>
{
    var program = await service.GetProgramByIdAsync(id, ct);

    if (!program.Success)
        return program.ToHttpResult();

    var programId = new DeleteProgramInput(program.Value!.Id);

    var deleted = await service.DeleteProgramAsync(programId, ct);

    return deleted.ToHttpResult();
});


app.Run();
