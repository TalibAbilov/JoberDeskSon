using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.User
{
    public class SubmitRegistrationException : Exception
    {
        public SubmitRegistrationException(string? message="Bu hesab artıq təsdiqlənib.") : base(message)
        {
        }
    }
}
