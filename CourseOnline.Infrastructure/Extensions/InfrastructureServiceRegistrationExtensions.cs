using CourseOnline.Application.Teachers.Interfaces;
using CourseOnline.Infrastructure.Persistence.Contexts;
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

        return services;
    }
}
