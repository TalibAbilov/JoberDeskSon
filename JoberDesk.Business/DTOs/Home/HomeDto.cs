using JoberDesk.Business.DTOs.Category;
using JoberDesk.Business.DTOs.Company;
using JoberDesk.Business.DTOs.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Home
{
	public record HomeDto
	{
		public int CompanyCount {  get; set; }
		public int JobCount { get; set; }
		public JobPaginationDto JobPaginationDto { get; set; }
		public List<GetCategoryDto>Categories { get; set; }
	}
}
