using JoberDesk.Business.DTOs.Job;
using JoberDesk.Core.Entities;
using JoberDesk.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Company
{
	public record GetCompanyDto
	{
		public int Id { get; set; }
		public string AppUserId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
		public string Website { get; set; }
		public string About { get; set; }
		public string Logo { get; set; }
        public ICollection<IndustryCompany> IndustryCompanies { get; set; }
        public ICollection<GetJobDto> Jobs { get; set; }
	}

}
