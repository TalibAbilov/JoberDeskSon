using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Company
{
    public class CompanyLogoException : Exception
    {
        public CompanyLogoException(string? message="Logo əlavə edilmədi.") : base(message)
        {
        }
    }
}
