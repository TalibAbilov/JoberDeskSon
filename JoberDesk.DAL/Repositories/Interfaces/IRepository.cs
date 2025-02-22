using JoberDesk.Core.Entities;
using JoberDesk.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public DbSet<TEntity> Table { get; }
        public Task<TEntity?> GetById(int id, params string[] includes);
        public IQueryable<TEntity> GetAll(params string[] includes);
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression = null, params string[] includes);
        public Task<TEntity> Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public void SoftDelete(TEntity entity);
        public Task<int> SaveChangesAsync();
        public Task<bool> IsExist(Expression<Func<TEntity, bool>> expression);
        Task AddRange(IEnumerable<TEntity> entities);
        public void RemoveRange(IEnumerable<TEntity> entities);

    }
}
