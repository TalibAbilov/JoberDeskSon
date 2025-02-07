using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Category
{
    public class CategoryNameExsistException : Exception
    {
        public CategoryNameExsistException(string? message= "Bu kateqoriya mövcuddur!") : base(message)
        {
        }

    }
}
