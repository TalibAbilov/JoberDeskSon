using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Category
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(string? message="Bu id-li kateqoriya mövcud deyil.") : base(message)
        {

        }
    }
}
