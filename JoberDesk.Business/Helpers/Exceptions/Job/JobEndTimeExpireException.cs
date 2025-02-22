using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Job
{
    public class JobEndTimeExpireException : Exception
    {
        public JobEndTimeExpireException(string? message="Vakansiyanın bitmə tarixi 1 aydan çox ola bilməz.") : base(message)
        {
        }
    }
}
