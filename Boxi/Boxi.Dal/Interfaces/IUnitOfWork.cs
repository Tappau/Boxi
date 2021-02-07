using System;
using System.Threading.Tasks;
using Boxi.Core.Domain;

namespace Boxi.Dal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Item> ItemRepo { get; }
        IBoxRepository BoxRepo { get; }
        Task<int> SaveAsync();
    }
}