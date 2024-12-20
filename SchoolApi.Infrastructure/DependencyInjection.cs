using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolApi.Infrastructure.Data;
using SchoolApi.Infrastructure.Interfaces;
using SchoolApi.Infrastructure.Repositories;


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
            return services;
        }
    }
}