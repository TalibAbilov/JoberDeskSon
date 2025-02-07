using FluentValidation.AspNetCore;
using JoberDesk.Business.Services.Implementations;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.DAL.Repositories.Implementations;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business
{
    public static class BusinessServiceRegistrations
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IIndustryService,IndustryService>();
            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(BusinessServiceRegistrations));
        }
    }
}
