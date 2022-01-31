using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected MovieAggregatorDbContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;

        public GenericRepository(
            MovieAggregatorDbContext context,
            ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<EntityEntry<T>> Add(T entity)
        {
           var entityEntry = await dbSet.AddAsync(entity);
           
            return entityEntry;
        }

        public virtual T Delete(T entity)
        {
            return dbSet.Remove(entity).Entity;    
        }

        public virtual  IQueryable<T> Get(bool tracking = false)
        {
            return  tracking ?  dbSet.AsQueryable() :  dbSet.AsNoTracking();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public EntityEntry<T> Update(T entity)
        {
            return dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
             dbSet.UpdateRange(entities);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }
    }
}
