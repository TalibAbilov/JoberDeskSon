using JoberDesk.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Core.Entities
{
	public class Industry:BaseEntity
	{
		public string Name { get; set; }
        public string Icon { get; set; }
        public ICollection<IndustryCompany> IndustryCompanies { get; set; }
	}
}
