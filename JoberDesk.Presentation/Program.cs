using JoberDesk.Business;
using JoberDesk.Core.Entities;
using JoberDesk.DAL;
using JoberDesk.DAL.Context;
using JoberDesk.DAL.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace JoberDesk.Presentation
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddBusinessServices();
			builder.Services.AddDALServices();

			builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
			{
				opt.User.RequireUniqueEmail = true;
				opt.Password.RequiredLength = 8;
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
				opt.Lockout.AllowedForNewUsers = true;
				opt.Lockout.MaxFailedAccessAttempts = 3;
			}).AddEntityFrameworkStores<JoberDeskDbContext>().AddDefaultTokenProviders();

			builder.Services.AddDbContext<JoberDeskDbContext>(opt =>
			{
				opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
			});
			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var logger = services.GetRequiredService<ILogger<Program>>();

				try
				{
					
					await SeedData.SeedDataAsync(app,services);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "An error occurred while seeding the database.");
				}
			}
			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
