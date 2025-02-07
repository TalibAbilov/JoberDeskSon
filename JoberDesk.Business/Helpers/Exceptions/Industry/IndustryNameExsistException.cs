using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Industry
{
    public class IndustryNameExsistException:Exception
    {
        public IndustryNameExsistException(string? message = "Bu sənaye mövcuddur!") : base(message)
        {
        }
    }
}
