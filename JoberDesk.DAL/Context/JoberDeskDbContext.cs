using JoberDesk.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL.Context
{
	public class JoberDeskDbContext : IdentityDbContext<AppUser>
	{
		public JoberDeskDbContext(DbContextOptions<JoberDeskDbContext> options) : base(options)
		{
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Industry> Industries { get; set; }
		public DbSet<IndustryCompany> IndustriesCompanies { get;set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<Setting> Settings { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(builder);
		}
	}
}
