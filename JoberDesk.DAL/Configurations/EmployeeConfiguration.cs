using JoberDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL.Configurations
{
	public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
            builder
                .Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);
            builder
				.Property(x => x.WorkAt)
				.HasMaxLength(200);
			builder
				.Property(x=>x.ImgUrl)
				.IsRequired()
				.HasMaxLength(100);
			builder
				.Property(x=>x.Company)
				.HasMaxLength(200);
			builder
				.Property(x => x.About)
				.HasMaxLength(250);
			builder
				.HasOne(x => x.AppUser)
				.WithOne(x => x.Employee)
				.HasForeignKey<Employee>(x => x.AppUserId);
		}
	}
}
