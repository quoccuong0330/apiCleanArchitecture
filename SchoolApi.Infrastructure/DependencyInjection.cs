using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolApi.Application.Services;
using SchoolApi.Domain.InterfaceRepositories;
using SchoolApi.Infrastructure.Data;
using SchoolApi.Infrastructure.Interfaces;
using SchoolApi.Infrastructure.Repositories;
using SchoolApi.Infrastructure.Security;


namespace SchoolApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("SchoolApi.Infrastructure")); // Migration ở Infrastructure
            });

            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPointRepository, PointRepository>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            return services;
        }
    }
}