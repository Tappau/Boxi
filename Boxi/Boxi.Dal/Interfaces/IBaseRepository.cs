using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boxi.Dal.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T updatedEntity);
        void Delete(T entity);
        void Delete(int id);
        void Delete(Expression<Func<T, bool>> predicate);

        Task<T> FetchAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FetchAllAsync(Expression<Func<T, bool>> predicate);

        Task<int> TotalCountAsync();
    }
}
