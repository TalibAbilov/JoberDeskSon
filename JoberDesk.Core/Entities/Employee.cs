using JoberDesk.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Core.Entities
{
	public class Employee:BaseEntity
	{
		public string FullName { get; set; }
		public string WorkAt { get; set; } = "İşsiz";
		public string ImgUrl { get; set; }
		public string Company { get; set; } = "İşsizdir və ya qeyd olunmayıb!";
		public string LinkedInLink { get; set; }
		public string Skills { get; set; } = "Bacarıqlar qeyd olunmayıb";
		public string About { get; set; } = "--";
		public bool IsConfirmedByAdmin { get; set; }
		//public DateTime? DateOfBirth { get; set; }
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
	}
}
