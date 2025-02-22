using JoberDesk.Business.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.DTOs.Job
{
	public record JobPaginationDto
	{
		public List<GetJobDto>? Jobs { get; set; }
		public int TotalJobs { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
	}
}
