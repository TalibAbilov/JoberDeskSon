using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Company
{
    public class CompanyNotFounException : Exception
    {
        public CompanyNotFounException(string? message= "Bu id-li company mövcud deyil!") : base(message)
        {
        }
    }
}
