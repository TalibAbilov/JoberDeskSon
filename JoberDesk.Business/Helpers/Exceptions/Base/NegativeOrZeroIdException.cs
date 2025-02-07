using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Base
{
    public class NegativeOrZeroIdException : Exception
    {
        public NegativeOrZeroIdException(string? message="Id müsbət olmalıdır!") : base(message)
        {
        }
    }
}
