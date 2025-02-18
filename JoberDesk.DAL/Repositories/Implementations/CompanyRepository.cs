using JoberDesk.Core.Entities;
using JoberDesk.DAL.Context;
using JoberDesk.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL.Repositories.Implementations
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(JoberDeskDbContext context) : base(context)
        {
        }
    }
}
