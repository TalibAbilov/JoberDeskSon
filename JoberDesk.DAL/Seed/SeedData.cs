using JoberDesk.Core.Entities;
using JoberDesk.DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL.Seed
{
	public static class SeedData
	{
		public async static Task SeedDataAsync(this IApplicationBuilder app, IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetRequiredService<JoberDeskDbContext>();
			var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


			var isAdminExist = await userManager.FindByEmailAsync("ebilovtalib43@gmail.com");

			if (isAdminExist == null)
			{
				var isAdminRoleExist = await roleManager.RoleExistsAsync("Admin");
				if (isAdminRoleExist == false)
				{
					await roleManager.CreateAsync(new IdentityRole("Admin"));
				}

				var admin = new AppUser()
				{
					RegistrationTime = DateTime.Now,
					Email = "ebilovtalib43@gmail.com",
					UserName = "admin",
					UserType=null,
					EmailConfirmed = true,
				};

				var user = await userManager.CreateAsync(admin, "Admin123@");

				if (!user.Succeeded)
				{
					throw new Exception("Error while creating admin");
				}

				await userManager.AddToRoleAsync(admin, "Admin");
			}

			await context.SaveChangesAsync();
		}
	}
}

