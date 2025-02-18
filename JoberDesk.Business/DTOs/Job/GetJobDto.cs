using JoberDesk.Core.Entities;
using JoberDesk.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Job
{
	public record GetJobDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Requirements { get; set; }
		public int CategoryId { get; set; }
		public JoberDesk.Core.Entities.Category Category { get; set; }
		public int CompanyId { get; set; }
		public EmploymentType EmploymentType { get; set; }
		public ExperienceLevel ExperienceLevel { get; set; }
		public EducationLevel EducationLevel { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime EndTime { get; set; }
        public bool IsPremium { get; set; }
        public DateTime PremiumEndTime { get; set; }
    }
}
