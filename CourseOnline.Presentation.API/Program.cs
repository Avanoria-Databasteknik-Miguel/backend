using CourseOnline.Application.Contracts.Courses;
using CourseOnline.Application.Contracts.Programs;
using CourseOnline.Application.Contracts.Students;
using CourseOnline.Application.Contracts.Teachers;
using CourseOnline.Application.Courses.DTOs.Inputs;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Application.Students.DTOs;
using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Infrastructure.Extensions;
using CourseOnline.Presentation.API.Common;
using CourseOnline.Presentation.API.Models.Courses;
using CourseOnline.Presentation.API.Models.Programs;
using CourseOnline.Presentation.API.Models.Students;




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



//      ##### STUDENTS #####


app.MapPost("/api/students", async (
    CreateStudentRequest req,
    IStudentService service,
    CancellationToken ct) =>
{
    var ToInput = new CreateStudentInput(req.FirstName, req.LastName, req.Email, req.ImageUrl, req.ProgramId);

    var student = await service.CreateStudentAsync(ToInput, ct);

    if (student.Success)
        return Results.Created($"/api/students/{student.Value!.Id}", student.Value);

    return student.ToHttpResult();

});

app.MapGet("/api/students", async (IStudentService service, CancellationToken ct) =>
{
    var students = await service.GetStudentsAsync(ct);

    return students.ToHttpResult();
});

app.MapPut("/api/students/{id:Guid}", async (Guid id, UpdateStudentRequest request, IStudentService service, CancellationToken ct) =>
{
    var student = await service.GetStudentByIdAsync(id, ct);

    if (!student.Success) return student.ToHttpResult();

    var updated = await service.UpdateStudentAsync(new UpdateStudentInput(id, request.FirstName, request.LastName, request.Email, request.ImageUrl, request.ProgramId), ct);

    return updated.ToHttpResult();
});

app.MapDelete("/api/students/{id:Guid}", async (Guid id, IStudentService service, CancellationToken ct) =>
{
    var student = await service.GetStudentByIdAsync(id, ct);

    if (!student.Success) return student.ToHttpResult();

    var deleted = await service.DeleteStudentAsync(id, ct);

    return deleted.ToHttpResult();
});


//      ##### COURSES #####

app.MapPost("/api/courses", async (CreateCourseRequest input, ICourseService service, CancellationToken ct) =>
{
var toInput = new CreateCourseInput(input.Name, input.DurationWeeks, input.MaxStudents, input.TeacherId, input.ProgramId);

var course = await service.CreateCourseAsync(toInput, ct);

    return course.Success ? Results.Created($"/api/courses/{course.Value!.Id}", course.Value) : course.ToHttpResult();
});

app.MapGet("/api/courses", async (ICourseService serivce, CancellationToken ct) => {
    var courses = await serivce.GetAllCoursesAsync(ct);

    return courses.ToHttpResult();
});

app.MapPut("/api/courses/{id:Guid}", async (Guid id, UpdateCourseInput input, ICourseService service, CancellationToken ct) =>
{

    var cmd = input with { Id = id };

    var course = await service.GetCourseByIdAsync(cmd.Id, ct);

    if (!course.Success) return course.ToHttpResult();

    var updated = await service.UpdateCourseAsync(cmd, ct);
    return updated.ToHttpResult();
});

app.MapDelete("/api/courses/{id:Guid}", async (Guid id, ICourseService service, CancellationToken ct) =>
{
    var course = await service.GetCourseByIdAsync(id, ct);

    if (!course.Success) return course.ToHttpResult();

    var deleted = await service.DeleteCourseAsync(new DeleteCourseInput(id), ct);

    return deleted.ToHttpResult();
});

app.Run();
