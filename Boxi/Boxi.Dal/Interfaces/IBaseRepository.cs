using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boxi.Dal.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        Task AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T updatedEntity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(int id);
        void DeleteRange(IEnumerable<T> entities);

        T Fetch(Expression<Func<T, bool>> predicate);
        Task<T> FetchAsync(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FetchAll(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FetchAllAsync(Expression<Func<T, bool>> predicate);
        
        bool Exist(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        int TotalCount();
        Task<int> TotalCountAsync();
    }
}