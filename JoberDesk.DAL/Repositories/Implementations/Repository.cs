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

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression = null, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return expression != null ? query.Where(expression) : query;
        }


        public IQueryable<TEntity> GetAll(params string[] includes)
        {
            IQueryable<TEntity> query = Table.AsNoTracking();
            foreach(var item in includes)
            {
                query=query.Include(item);
            }
            return query;
        }

        public async Task<TEntity?> GetById(int id, params string[] includes)
        {
            IQueryable<TEntity> query = Table.AsNoTracking();
            foreach(var item in includes)
            {
                query=query.Include(item);
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> expression)
        {
            return await Table.AnyAsync(expression);
        }

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			_context.RemoveRange(entities);
		}

		public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            Table.Update(entity);
        }
            
        public void Update(TEntity entity)
        {
            Table.Update(entity);
        }
    }
}
