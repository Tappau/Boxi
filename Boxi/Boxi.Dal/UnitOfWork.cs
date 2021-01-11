using System;
using System.Threading.Tasks;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;

namespace Boxi.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ComicInventoryContext _context;

        public IBaseRepository<Grade> GradeRepo { get; set; }
        public IBoxStoreRepository BoxRepo { get; set; }
        public IPublisherRepository PublisherRepo { get; set; }

        public UnitOfWork(ComicInventoryContext comicInventoryContext, IBaseRepository<Grade> gradeRepo
        ,IPublisherRepository publisherRepository, IBoxStoreRepository boxStoreRepository)
        {
            _context = comicInventoryContext;
            GradeRepo = gradeRepo;
            BoxRepo = boxStoreRepository;
            PublisherRepo = publisherRepository;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _context.Dispose();
        }
    }
}