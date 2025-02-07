using JoberDesk.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Core.Entities
{
	public class IndustryCompany:BaseEntity
	{
		public int IndustryId { get; set; }
		public Industry Industry { get; set; }
		public int CompanyId { get; set; }
		public Company Company { get; set; }
	}
}
