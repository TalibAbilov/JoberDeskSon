using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.User
{
    public class UserRegisterException : Exception
    {
        public UserRegisterException(string? message) : base(message)
        {
        }
    }
}
