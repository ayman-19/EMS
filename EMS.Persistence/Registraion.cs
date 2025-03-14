using EMS.Domain.Abstraction;
using EMS.Persistence.Context;
using EMS.Persistence.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace EMS.Persistence
{
    public static class Registeration
    {
        public static IServiceCollection RegisterPersistenceDependancies(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            string connectionString = configuration.GetConnectionString("EMS_CONNECTIONSTRING");

            services.AddDbContext<EMSDbContext>(cfg =>
            {
                cfg.UseSqlServer(connectionString);
            });
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IEmailSender, EmailSender>()
                .AddScoped<ITokenRepository, TokenRepository>()
                .AddScoped<IJWTManager, JWTManager>()
                .AddScoped<IDepartmentRepository, DepartmentRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IPositionRepository, PositionRepository>()
                .AddScoped<ITokenRepository, TokenRepository>()
                .AddScoped<IJobs, Jobs>();
            services
                .AddQuartz(q =>
                {
                    q.UsePersistentStore(op =>
                    {
                        op.UseSqlServer(cstr => cstr.ConnectionString = connectionString);
                        op.UseNewtonsoftJsonSerializer();
                        op.UseProperties = true;
                        op.PerformSchemaValidation = true;
                    });
                })
                .AddQuartzHostedService(h => h.WaitForJobsToComplete = true);
            services.AddAuthentication();
            return services;
        }
    }
}
