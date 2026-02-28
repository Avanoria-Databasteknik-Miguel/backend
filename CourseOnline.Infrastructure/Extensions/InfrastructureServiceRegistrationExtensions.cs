using CourseOnline.Application.Contracts.Courses;
using CourseOnline.Application.Contracts.Programs;
using CourseOnline.Application.Contracts.Students;
using CourseOnline.Application.Contracts.Teachers;
using CourseOnline.Application.Courses.Interfaces;
using CourseOnline.Application.Programs.Interfaces;
using CourseOnline.Application.Services;
using CourseOnline.Application.Students.Interfaces;
using CourseOnline.Application.Teachers.Interfaces;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Courses;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Programs;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Students;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CourseOnline.Infrastructure.Extensions;
public static class InfrastructureServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<CourseOnlineDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CourseOnlineDatabase")));
   

        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ITeacherService, TeacherService>();

        services.AddScoped<IProgramRepository, ProgramRepository>();
        services.AddScoped<IProgramService, ProgramService>();

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseService, CourseService>();


        return services;
    }
}
