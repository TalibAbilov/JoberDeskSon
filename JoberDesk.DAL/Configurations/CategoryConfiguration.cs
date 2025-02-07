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
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder
				.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);
            builder
                .Property(x => x.Icon)
                .HasMaxLength(100);
            builder
				.HasMany(x => x.Jobs)
				.WithOne(x => x.Category)
				.HasForeignKey(x => x.CategoryId);
		}
	}
}
