using JoberDesk.Core.Entities.Base;
using JoberDesk.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Core.Entities
{
	public class Company:BaseEntity
	{
		public string CompanyName { get; set; }
		public string? Address { get; set; }
		public string? Website { get; set; }
		public string About { get; set; }
		public string Logo { get; set; }
		public bool IsConfirmedByAdmin { get; set; }
		public ICollection<IndustryCompany> IndustryCompanies { get; set; }
		public ICollection<Job> Jobs { get; set; }
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public bool IsPremium { get; set; }
        public DateTime? PremiumUntil { get; set; }

    }
}
