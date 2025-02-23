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
	public class JobConfiguration : IEntityTypeConfiguration<Job>
	{
		public void Configure(EntityTypeBuilder<Job> builder)
		{
			builder
				.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(250);
            builder
                .Property(x => x.Location)
                .IsRequired()
                .HasMaxLength(250);
            builder
				.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(2000);
			builder
				.Property(x => x.Requirements)
				.IsRequired()
				.HasMaxLength(2000);
			builder
				.Property(x => x.EmploymentType)
				.IsRequired();
			builder
				.Property(x => x.EducationLevel)
				.IsRequired();
			builder
				.Property(x => x.ExperienceLevel)
				.IsRequired();
			builder
				.Property(x=>x.EndTime)
				.IsRequired();
            builder
			   .HasOne(x => x.Category)
			   .WithMany(x=>x.Jobs) 
			   .HasForeignKey(x => x.CategoryId)
			   .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Company)
                .WithMany(x=>x.Jobs)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

        }
	}
}
