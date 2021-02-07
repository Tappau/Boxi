using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boxi.Dal.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T updatedEntity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(int id);
        void DeleteRange(IEnumerable<T> entities);

        Task<T> FetchAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FetchAllAsync(Expression<Func<T, bool>> predicate);
        
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        Task<int> TotalCountAsync();
    }
}