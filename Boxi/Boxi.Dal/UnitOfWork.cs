using System;
using System.Threading.Tasks;
using Boxi.Core.Domain;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;

namespace Boxi.Dal
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly BoxiDataContext _context;

        public IBaseRepository<Item> ItemRepo { get; }
        public IBoxRepository BoxRepo { get; }

        public UnitOfWork(BoxiDataContext comicInventoryContext, IBoxRepository boxStoreRepository
        , IBaseRepository<Item> itemRepository)
        {
            _context = comicInventoryContext;
            BoxRepo = boxStoreRepository;
            ItemRepo = itemRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _context.Dispose();
        }
    }
}