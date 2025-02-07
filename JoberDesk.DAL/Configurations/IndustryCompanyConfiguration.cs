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
	public class IndustryCompanyConfiguration : IEntityTypeConfiguration<IndustryCompany>
	{
		public void Configure(EntityTypeBuilder<IndustryCompany> builder)
		{
			builder
				.HasOne(x => x.Industry)
				.WithMany(x => x.IndustryCompanies)
				.HasForeignKey(x => x.IndustryId);
			builder
				.HasOne(x => x.Company)
				.WithMany(x => x.IndustryCompanies)
				.HasForeignKey(x => x.CompanyId);
		}
	}
}
