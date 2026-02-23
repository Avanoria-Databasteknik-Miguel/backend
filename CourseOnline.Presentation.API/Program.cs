using CourseOnline.Infrastructure.Extensions;
using CourseOnline.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();


builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.Run();
