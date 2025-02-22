using JoberDesk.Core.Entities.Base;
using JoberDesk.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Core.Entities
{
	public class Job:BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public  string Requirements { get; set; }
		public EmploymentType EmploymentType { get; set; }
		public ExperienceLevel ExperienceLevel { get; set; }
		public EducationLevel EducationLevel { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }	
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public DateTime EndTime { get; set; }
		public bool IsPremium { get; set; }
		public DateTime PremiumEndTime { get; set; }
		public string Location { get; set; }
		public int Salary {  get; set; }
		public int ViewCount { get; set; }
		public DateTime? LastReActivateTime { get; set; }
    }
}
