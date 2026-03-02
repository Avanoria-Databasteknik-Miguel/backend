using CourseOnline.Application.Categories.Interfaces;
using CourseOnline.Application.Classrooms.Interfaces;
using CourseOnline.Application.Common.Interfaces;
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
using CourseOnline.Application.CourseCategories.Interfaces;
using CourseOnline.Application.Courses.Interfaces;
using CourseOnline.Application.CourseSessions.Interfaces;
using CourseOnline.Application.Floors.Interfaces;
using CourseOnline.Application.Programs.Interfaces;
using CourseOnline.Application.Registrations.Interfaces;
using CourseOnline.Application.Reports.Interfaces;
using CourseOnline.Application.Services;
using CourseOnline.Application.StudentCourses.Interfaces;
using CourseOnline.Application.Students.Interfaces;
using CourseOnline.Application.Teachers.Interfaces;
using CourseOnline.Domain.Models;
using CourseOnline.Infrastructure.Persistence.Contexts;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Categories;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Classrooms;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.CourseCategories;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Courses;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.CourseSessions;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.CourseSessionStudents;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Floors;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Programs;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Reports;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.StudentCourses;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Students;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Teachers;
using CourseOnline.Infrastructure.Persistence.EFC.UnitOfWork;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace CourseOnline.Infrastructure.Extensions;
public static class InfrastructureServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        if (env.IsDevelopment())
        {
            services.AddSingleton(_ =>
            {
                var connectionString = configuration.GetConnectionString("DevelopmentDatabase") ?? "Data Source =:memory:; Cache = Shared";

                var conn = new SqliteConnection(connectionString);
                conn.Open();
                return conn;
            });

            services.AddDbContext<CourseOnlineDbContext>((sp, options) =>
            {
                var conn = sp.GetRequiredService<SqliteConnection>();
                options.UseSqlite(conn);
            });
        }
        else
        {
            var connectionString = configuration.GetConnectionString("CourseOnlineDatabase") ?? throw new InvalidOperationException("Missing CourseOnlineDatabase connection string.");

            services.AddDbContext<CourseOnlineDbContext>(options => options.UseSqlServer(connectionString));
        }


   

        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ITeacherService, TeacherService>();

        services.AddScoped<IProgramRepository, ProgramRepository>();
        services.AddScoped<IProgramService, ProgramService>();

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseService, CourseService>();

        services.AddScoped<ICourseSessionRepository, CourseSessionRepository>();
        services.AddScoped<ICourseSessionService, CourseSessionService>();

        services.AddScoped<IClassroomsRepository, ClassroomRepository>();
        services.AddScoped<IClassroomService, ClassroomService>();

        services.AddScoped<IFloorRepository, FloorRepository>();
        services.AddScoped<IFloorService, FloorService>();

        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddScoped<IRegistrationService, RegistrationService>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
        services.AddScoped<ICourseCategoryService, CourseCategoryService>();

        services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
        services.AddScoped<IStudentCourseService, StudentCourseService>();

        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IReportService, ReportService>();

        services.AddScoped<IUnitOfWork, EfcUnitOfWork>();

        

        return services;
    }
}
