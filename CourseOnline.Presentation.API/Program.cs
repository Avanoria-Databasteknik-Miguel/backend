using CourseOnline.Application.Contracts.Teachers;
using CourseOnline.Application.Teachers.DTOs.Inputs;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Extensions;
using CourseOnline.Infrastructure.Persistence;
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


app.Run();
