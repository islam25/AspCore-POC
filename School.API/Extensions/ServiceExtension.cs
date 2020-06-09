using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Core.ILoggerService;
using School.Core.Interfaces;
using School.Core.Services;
using School.Infrastructure.Context;
using School.Infrastructure.Repository;
using School.LoggerService;

namespace School.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy("cors", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyMethod();
                });
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("sqlConnection");
            services.AddDbContext<SchoolContext>(opt => opt.UseSqlServer(connectionString));
        }

        public static void ConfigureDI(this IServiceCollection services) 
        {
            services.AddScoped(typeof(IRepository<>) , typeof(Repository<>));
            services.AddScoped<IStudentService , StudentService>();
            services.AddScoped<IStudentCourseService , StudentCourseService>();
            services.AddScoped<ICourseService , CourseService>();
            services.AddScoped<IDepartmentService , DepartmentService>();
        }
    }
}
