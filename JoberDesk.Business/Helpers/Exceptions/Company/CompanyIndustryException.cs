using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Company
{
    public class CompanyIndustryException : Exception
    {
        public CompanyIndustryException(string? message="Sənaye tiplərini düzgün seç.") : base(message)
        {
        }
    }
}
