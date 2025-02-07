using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.User
{
	public class UserLoginException : Exception
	{
		public UserLoginException(string? message="Hesaba giriş zamanı xəta.") : base(message)
		{

		}
	}
}
