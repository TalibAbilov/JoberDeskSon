using JoberDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Industry
{
	public record GetIndustryDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        //public ICollection<IndustryCompany>IndustryCompanies { get; set; }
    }
}
