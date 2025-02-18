using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Company
{
    public class CompanyAlreadyExistsException : Exception
    {
        public CompanyAlreadyExistsException(string? message= "İstifadəçinin artıq bir şirkəti var.") : base(message) { }

    }

}
