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
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{

            builder
                .Property(x => x.CompanyName)
				.IsRequired()
                .HasMaxLength(100);
            builder
				.Property(x => x.Address)
				.HasMaxLength(250);
			builder
				.Property(x => x.Website)
				.HasMaxLength(250);
			builder
				.Property(x => x.About)
				.IsRequired()
				.HasMaxLength(1000);
			builder
				.Property(x => x.Logo)
				.IsRequired()
				.HasMaxLength(100);
			builder.
				HasMany(x => x.IndustryCompanies)
				.WithOne(x => x.Company)
				.HasForeignKey(x => x.CompanyId);
			builder
				.HasMany(x=>x.Jobs)
				.WithOne(x=>x.Company)
				.HasForeignKey(x=>x.CompanyId);
			builder
				.HasOne(x => x.AppUser)
				.WithOne(x => x.Company)
				.HasForeignKey<Company>(x => x.AppUserId);
		}
	}
}
