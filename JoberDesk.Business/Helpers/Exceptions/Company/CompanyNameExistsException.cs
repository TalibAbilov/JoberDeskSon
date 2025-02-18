using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Company
{
    public class CompanyNameExistsException : Exception
    {
        public CompanyNameExistsException(string? message= "Bu adda şirkət mövcuddur.") : base(message)
        {
        }
    }
}
