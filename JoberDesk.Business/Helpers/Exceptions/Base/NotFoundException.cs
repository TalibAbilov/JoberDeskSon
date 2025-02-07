using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Base
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message="Tapılmadı") : base(message)
        {
        }
    }
}
