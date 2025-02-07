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
	public class IndustryConfiguration : IEntityTypeConfiguration<Industry>
	{
		public void Configure(EntityTypeBuilder<Industry> builder)
		{
			builder
				.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);
            builder
                .Property(x => x.Icon)
                .HasMaxLength(100);
            builder
				.HasMany(x => x.IndustryCompanies)
				.WithOne(x => x.Industry)
				.HasForeignKey(x => x.IndustryId);
		}
	}
}
