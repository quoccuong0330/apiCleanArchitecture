using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolApi.Application;
using SchoolApi.Domain;
using SchoolApi.Infrastructure;
using SchoolApi.Infrastructure.Data;

namespace SchoolApi.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services) {
            services.AddApplicationDI();
            return services;
        }
    }
}