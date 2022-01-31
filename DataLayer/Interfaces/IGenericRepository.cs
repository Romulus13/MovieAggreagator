using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Get(bool tracking = false);
        Task<T?> GetById(Guid id);
        Task<EntityEntry<T>> Add(T entity);

        void UpdateRange(IEnumerable<T> entities);
        Task AddRange(IEnumerable<T> entities);
        T Delete(T entity);
        EntityEntry<T> Update(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }

}
