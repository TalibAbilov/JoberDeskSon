using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.User
{
    public class ResetPasswordException : Exception
    {
        public ResetPasswordException(string? message="Şifrə yenilənmədi.") : base(message)
        {
        }
    }
}
