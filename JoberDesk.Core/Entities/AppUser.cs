using JoberDesk.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Core.Entities
{
	public class AppUser:IdentityUser
	{
		public UserType? UserType { get; set; }
		public Company? Company { get; set; }
		public Employee? Employee { get; set; }
		public DateTime RegistrationTime { get; set; }=DateTime.Now;	
	}
}
