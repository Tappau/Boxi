using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Boxi.Dal.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly BoxiDataContext Context;

        public BaseRepository(BoxiDataContext context)
        {
            Context = context;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
        }

        public virtual void Update(T updatedEntity)
        {
            Context.Set<T>().Update(updatedEntity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var itemtoRemove = Get(id);
            Context.Set<T>().Remove(itemtoRemove);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public virtual async Task<T> FetchAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> FetchAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().AnyAsync(predicate);
        }

        public virtual async Task<int> TotalCountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            Context.Dispose();
        }
    }
}