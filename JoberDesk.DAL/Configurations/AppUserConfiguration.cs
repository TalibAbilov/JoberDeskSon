using JoberDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL.Configurations
{
	public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder
				.HasOne(x => x.Employee)
				.WithOne(x => x.AppUser)
				.HasForeignKey<Employee>(x => x.AppUserId)
				.OnDelete(DeleteBehavior.Cascade);
			builder
				.HasOne(x => x.Company)
				.WithOne(x => x.AppUser)
				.HasForeignKey<Company>(x => x.AppUserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
