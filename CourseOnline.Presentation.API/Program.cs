using CourseOnline.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddDbContext<CourseOnlineDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("CourseOnlineDatabase"), 
        sql => sql
        .MigrationsAssembly("CourseOnline.Infrastructure")
    ));

var app = builder.Build();


app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.Run();
