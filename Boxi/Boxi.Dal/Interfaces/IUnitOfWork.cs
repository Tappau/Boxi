using System.Threading.Tasks;
using Boxi.Core.Domain;
using Boxi.Dal.Models;

namespace Boxi.Dal.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Item> ItemRepo { get; }
        IBoxRepository BoxRepo { get; }
        BoxiDataContext GetDataContext();
        Task<int> SaveAsync();
    }
}