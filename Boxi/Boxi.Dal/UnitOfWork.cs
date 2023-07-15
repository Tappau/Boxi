using System.Threading.Tasks;
using Boxi.Core.Domain;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Boxi.Dal.Repositories;

namespace Boxi.Dal
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly BoxiDataContext _context;

        public UnitOfWork(BoxiDataContext dataContext, IBoxRepository boxStoreRepository
          , IBaseRepository<Item> itemRepository)
        {
            _context = dataContext;
            BoxRepo = boxStoreRepository;
            ItemRepo = itemRepository;
        }

        /// <summary>
        /// Constructor used for UnitTests;
        /// </summary>
        /// <param name="dataContext"></param>
        public UnitOfWork(BoxiDataContext dataContext)
        {
            _context = dataContext;
            BoxRepo ??= new BoxRepository(dataContext);
            ItemRepo ??= new BaseRepository<Item>(dataContext);
        }

        public IBaseRepository<Item> ItemRepo { get; }
        public IBoxRepository BoxRepo { get; }

        /// <summary>
        /// Get the context to use directly for specific use cases.
        /// </summary>
        /// <returns></returns>
        public BoxiDataContext GetDataContext()
        {
            return _context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
