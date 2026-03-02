using CourseOnline.Application.Categories.DTOs.Inputs;
using CourseOnline.Application.Classrooms.DTOs;
using CourseOnline.Application.Contracts.Categories;
using CourseOnline.Application.Contracts.Classrooms;
using CourseOnline.Application.Contracts.CourseCategories;
using CourseOnline.Application.Contracts.Courses;
using CourseOnline.Application.Contracts.CourseSessions;
using CourseOnline.Application.Contracts.Floors;
using CourseOnline.Application.Contracts.Programs;
using CourseOnline.Application.Contracts.Registrations;
using CourseOnline.Application.Contracts.StudentCourses;
using CourseOnline.Application.Contracts.Students;
using CourseOnline.Application.Contracts.Teachers;
using CourseOnline.Application.Courses.DTOs.Inputs;
using CourseOnline.Application.CourseSessions.DTOs.Inputs;
using CourseOnline.Application.Floors.DTOs;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Application.Reports.Interfaces;
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


//      ##### COURSES SESSIONS #####

app.MapPost("/api/course-sessions", async (
    CreateCourseSessionInput input,
    ICourseSessionService service,
    CancellationToken ct) =>
{
var created = await service.CreateCourseSessionAsync(input, ct);

return created.Success
    ? Results.Created($"/api/course-sessions/{created.Value!.Id}", created.Value)
    : created.ToHttpResult();
});


app.MapGet("/api/course-sessions", async (
    ICourseSessionService service,
    CancellationToken ct) =>
{
var sessions = await service.GetAllCourseSessionsAsync(ct);

return sessions.ToHttpResult();
});

app.MapGet("/api/course-sessions/by-course/{courseId:Guid}", async (
    Guid courseId,
    ICourseSessionService service,
    CancellationToken ct) =>
{
    var sessions = await service.GetCourseSessionsByCourseIdAsync(courseId, ct);
    return sessions.ToHttpResult();
});


app.MapGet("/api/course-sessions/{id:Guid}", async (
    Guid id,
    ICourseSessionService service,
    CancellationToken ct) =>
{
var session = await service.GetCourseSessionByIdAsync(id, ct);

return session.ToHttpResult();
});


app.MapPut("/api/course-sessions/{id:Guid}", async (
    Guid id,
    UpdateCourseSessionInput input,
    ICourseSessionService service,
    CancellationToken ct) =>
{
var cmd = input with { Id = id };

var updated = await service.UpdateCourseSessionAsync(cmd, ct);

return updated.ToHttpResult();
});


app.MapDelete("/api/course-sessions/{id:Guid}", async (
    Guid id,
    ICourseSessionService service,
    CancellationToken ct) =>
{
    var deleted = await service.DeleteCourseSessionAsync(new DeleteCourseSessionInput(id), ct);

    return deleted.ToHttpResult();
});

// ##### CLASSROOMS #####

app.MapPost("/api/classrooms", async (CreateClassroomInput input, IClassroomService service, CancellationToken ct) =>
{
    var created = await service.CreateClassroomAsync(input, ct);

    return created.Success
        ? Results.Created($"/api/classrooms/{created.Value!.Id}", created.Value)
        : created.ToHttpResult();
});

app.MapGet("/api/classrooms", async (IClassroomService service, CancellationToken ct) =>
{
    var classrooms = await service.GetAllClassroomsAsync(ct);
    return classrooms.ToHttpResult();
});

app.MapGet("/api/classrooms/{id:int}", async (int id, IClassroomService service, CancellationToken ct) =>
{
    var classroom = await service.GetClassroomByIdAsync(id, ct);
    return classroom.ToHttpResult();
});

app.MapPut("/api/classrooms/{id:int}", async (int id, UpdateClassroomInput input, IClassroomService service, CancellationToken ct) =>
{
    var cmd = input with { Id = id };
    var updated = await service.UpdateClassroomAsync(cmd, ct);
    return updated.ToHttpResult();
});

app.MapDelete("/api/classrooms/{id:int}", async (int id, IClassroomService service, CancellationToken ct) =>
{
    var deleted = await service.DeleteClassroomAsync(new DeleteClassroomInput(id), ct);
    return deleted.ToHttpResult();
});


// ##### FLOORS #####

app.MapPost("/api/floors", async (CreateFloorInput input, IFloorService service, CancellationToken ct) =>
{
    var created = await service.CreateFloorAsync(input, ct);

    return created.Success
        ? Results.Created($"/api/floors/{created.Value!.Id}", created.Value)
        : created.ToHttpResult();
});

app.MapGet("/api/floors", async (IFloorService service, CancellationToken ct) =>
{
    var floors = await service.GetAllFloorsAsync(ct);
    return floors.ToHttpResult();
});

app.MapGet("/api/floors/{id:int}", async (int id, IFloorService service, CancellationToken ct) =>
{
    var floor = await service.GetFloorByIdAsync(id, ct);
    return floor.ToHttpResult();
});

app.MapGet("/api/floors/by-level/{level}", async (string level, IFloorService service, CancellationToken ct) =>
{
    var floor = await service.GetFloorByLevelAsync(level, ct);
    return floor.ToHttpResult();
});

app.MapPut("/api/floors/{id:int}", async (int id, UpdateFloorInput input, IFloorService service, CancellationToken ct) =>
{
    var cmd = input with { Id = id };
    var updated = await service.UpdateFloorAsync(cmd, ct);
    return updated.ToHttpResult();
});

app.MapDelete("/api/floors/{id:int}", async (int id, IFloorService service, CancellationToken ct) =>
{
    var deleted = await service.DeleteFloorAsync(id, ct);
    return deleted.ToHttpResult();
});


// ##### REGISTRATIONS/COURSE-SESSION-STUDENTS #####

app.MapPost("/api/course-sessions/{sessionId:Guid}/students/{studentId:Guid}", async (Guid sessionId, Guid studentId, IRegistrationService service, CancellationToken ct) =>
{
    var created = await service.RegisterStudentAsync(sessionId, studentId, ct);

    return created.Success
        ? Results.Created($"/api/course-sessions/{sessionId}/students/{studentId}", created.Value)
        : created.ToHttpResult();
});

app.MapDelete("/api/course-sessions/{sessionId:Guid}/students/{studentId:Guid}", async (Guid sessionId, Guid studentId, IRegistrationService service, CancellationToken ct) =>
{
    var deleted = await service.UnregisterStudentAsync(sessionId, studentId, ct);
    return deleted.ToHttpResult();
});

app.MapGet("/api/course-sessions/{sessionId:Guid}/students", async (Guid sessionId, IRegistrationService service, CancellationToken ct) =>
{
    var regs = await service.GetRegistrationsBySessionIdAsync(sessionId, ct);
    return regs.ToHttpResult();
});

app.MapGet("/api/students/{studentId:Guid}/course-sessions", async (Guid studentId, IRegistrationService service, CancellationToken ct) =>
{
    var regs = await service.GetRegistrationsByStudentIdAsync(studentId, ct);
    return regs.ToHttpResult();
});

// ##### CATEGORIES #####

app.MapPost("/api/categories", async (
    CreateCategoryInput input,
    ICategoryService service,
    CancellationToken ct) =>
{
    var created = await service.CreateCategoryAsync(input, ct);

    return created.Success
        ? Results.Created($"/api/categories/{created.Value!.Id}", created.Value)
        : created.ToHttpResult();
});

app.MapGet("/api/categories", async (
    ICategoryService service,
    CancellationToken ct) =>
{
    var cats = await service.GetAllCategoriesAsync(ct);
    return cats.ToHttpResult();
});

app.MapGet("/api/categories/{id:Guid}", async (
    Guid id,
    ICategoryService service,
    CancellationToken ct) =>
{
    var cat = await service.GetCategoryByIdAsync(id, ct);
    return cat.ToHttpResult();
});

app.MapPut("/api/categories/{id:Guid}", async (
    Guid id,
    UpdateCategoryInput input,
    ICategoryService service,
    CancellationToken ct) =>
{
    // antar att UpdateCategoryInput är en record med Id som går att sätta via "with"
    var cmd = input with { Id = id };

    var updated = await service.UpdateCategoryAsync(cmd, ct);
    return updated.ToHttpResult();
});

app.MapDelete("/api/categories/{id:Guid}", async (
    Guid id,
    ICategoryService service,
    CancellationToken ct) =>
{
    var deleted = await service.DeleteCategoryAsync(new DeleteCategoryInput(id), ct);
    return deleted.ToHttpResult();
});




// ##### COURSE-CATEGORIES #####

app.MapPost("/api/courses/{courseId:Guid}/categories/{categoryId:Guid}", async (
    Guid courseId,
    Guid categoryId,
    ICourseCategoryService service,
    CancellationToken ct) =>
{
    var result = await service.AddCategoryToCourseAsync(courseId, categoryId, ct);
    return result.ToHttpResult();
});

app.MapDelete("/api/courses/{courseId:Guid}/categories/{categoryId:Guid}", async (
    Guid courseId,
    Guid categoryId,
    ICourseCategoryService service,
    CancellationToken ct) =>
{
    var result = await service.RemoveCategoryFromCourseAsync(courseId, categoryId, ct);
    return result.ToHttpResult();
});

app.MapGet("/api/courses/{courseId:Guid}/categories", async (
    Guid courseId,
    ICourseCategoryService service,
    CancellationToken ct) =>
{
    var result = await service.GetCategoriesByCourseIdAsync(courseId, ct);
    return result.ToHttpResult();
});

// (valfritt) GET /api/categories/{categoryId}/courses
app.MapGet("/api/categories/{categoryId:Guid}/courses", async (
    Guid categoryId,
    ICourseCategoryService service,
    CancellationToken ct) =>
{
    var result = await service.GetCoursesByCategoryIdAsync(categoryId, ct);
    return result.ToHttpResult();
});

// ##### STUDENT-COURSES #####

app.MapPost("/api/students/{studentId:Guid}/courses/{courseId:Guid}", async (
    Guid studentId,
    Guid courseId,
    IStudentCourseService service,
    CancellationToken ct) =>
{
    var created = await service.AddStudentToCourseAsync(studentId, courseId, ct);
    return created.ToHttpResult();
});

app.MapDelete("/api/students/{studentId:Guid}/courses/{courseId:Guid}", async (
    Guid studentId,
    Guid courseId,
    IStudentCourseService service,
    CancellationToken ct) =>
{
    var deleted = await service.RemoveStudentFromCourseAsync(studentId, courseId, ct);
    return deleted.ToHttpResult();
});

app.MapGet("/api/students/{studentId:Guid}/courses", async (
    Guid studentId,
    IStudentCourseService service,
    CancellationToken ct) =>
{
    var courses = await service.GetCoursesByStudentIdAsync(studentId, ct);
    return courses.ToHttpResult();
});


app.MapGet("/api/courses/{courseId:Guid}/students", async (
    Guid courseId,
    IStudentCourseService service,
    CancellationToken ct) =>
{
    var students = await service.GetStudentsByCourseIdAsync(courseId, ct);
    return students.ToHttpResult();
});


// ##### REPORTS #####

app.MapGet("/api/reports/session-availability", async (
    IReportService service,
    CancellationToken ct) =>
{
    var result = await service.GetSessionAvailabilityAsync(ct);
    return Results.Ok(result);
});


app.MapGet("/api/reports/students/upcoming-sessions", async (
    IReportService service,
    CancellationToken ct) =>
{
    var result = await service.GetStudentWithUpcomingSessionsAsync(ct);
    return Results.Ok(result);
});

app.Run();
