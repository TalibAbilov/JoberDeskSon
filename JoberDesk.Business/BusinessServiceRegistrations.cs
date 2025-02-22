using FluentValidation.AspNetCore;
using JoberDesk.Business.Helpers.Email;
using JoberDesk.Business.Services.Implementations;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.DAL.Repositories.Implementations;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
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
        public static void AddBusinessServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IJobService,JobService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IIndustryService,IndustryService>();
            services.AddScoped<ICompanyService,CompanyService>();
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService,MailService>();
            services.AddScoped<ILayoutService,LayoutService>();
            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(BusinessServiceRegistrations));
            services.AddHostedService<PremiumJobCheckerService>();
            services.AddHostedService<JobEndTimeCheckerService>();


        }
    }
}
