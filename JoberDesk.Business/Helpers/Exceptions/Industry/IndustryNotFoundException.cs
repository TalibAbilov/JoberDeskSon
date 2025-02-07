using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Industry
{
    public class IndustryNotFoundException : Exception
    {
        public IndustryNotFoundException(string? message= "Bu id-li sənaye mövcud deyil.") : base(message)
        {
        }
    }
}
