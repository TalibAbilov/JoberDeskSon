using JoberDesk.Core.Entities.Base;
using JoberDesk.DAL.Context;
using JoberDesk.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly JoberDeskDbContext _context;

        public Repository(JoberDeskDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task<TEntity> Create(TEntity entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = Table.AsNoTracking();
            return query;
        }

        public async Task<TEntity?> GetById(int id)
        {
            IQueryable<TEntity> query = Table.AsNoTracking();
            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> expression)
        {
            return await Table.AnyAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            Table.Update(entity);
        }
    }
}
