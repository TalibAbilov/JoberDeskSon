using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Exceptions.Job
{
    public class PremiumEndGreaterThanEndTime : Exception
    {
        public PremiumEndGreaterThanEndTime(string? message="Bu vakansiyanın bitmə tarixinə 7 gündən az qalıb premium edə bilmərsiniz.") : base(message)
        {

        }
    }
}
