using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Job
{
	public class JobNotFoundException : Exception
	{
		public JobNotFoundException(string? message="Bu id-li vakansiya tapılmadı.") : base(message)
		{
		}
	}
}
