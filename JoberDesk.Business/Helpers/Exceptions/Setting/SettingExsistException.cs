using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Setting
{
    public class SettingExsistException : Exception
    {
        public SettingExsistException(string? message="Bu açar mövcuddur") : base(message)
        {
        }
    }
}
