using System;
using System.Threading.Tasks;
using Boxi.Dal.Models;

namespace Boxi.Dal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Grade> GradeRepo { get; set; }
        IBoxStoreRepository BoxRepo { get; set; }
        IPublisherRepository PublisherRepo { get; set; }
        int Save();
        Task<int> SaveAsync();
    }
}