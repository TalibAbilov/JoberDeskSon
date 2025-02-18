using JoberDesk.Core.Entities;
using JoberDesk.DAL.Context;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL.Repositories.Implementations
{
    public class LayoutRepository : Repository<Setting>, ILayoutRepository
    {
        public LayoutRepository(JoberDeskDbContext context) : base(context)
        {
        }

		public async Task<Setting?> GetByKey(string key)
		{

			return await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Key.ToLower()==key.ToLower());

		}
	}
}
