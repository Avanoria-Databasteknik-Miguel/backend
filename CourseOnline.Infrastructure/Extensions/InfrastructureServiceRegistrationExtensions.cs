using CourseOnline.Infrastructure.Persistence;
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
   

        //TODO: Add repositories t.ex:  services.AddScoped<IInstructorRepository, InstructorRepository>();

        return services;
    }
}
