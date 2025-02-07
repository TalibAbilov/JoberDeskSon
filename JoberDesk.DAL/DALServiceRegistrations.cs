using JoberDesk.DAL.Repositories.Implementations;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL
{
    public static class DALServiceRegistrations
    {
        public static void AddDALServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IIndustryRepository, IndustryRepository>();
        }
    }
}
