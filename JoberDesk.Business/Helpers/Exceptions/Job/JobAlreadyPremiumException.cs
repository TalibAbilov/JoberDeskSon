using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Job
{
    public class JobAlreadyPremiumException : Exception
    {
        public JobAlreadyPremiumException(string? message) : base(message)
        {
        }
    }
}
