using System.Threading.Tasks;
using Boxi.Dal.Models;

namespace Boxi.Dal.Interfaces
{
    public interface IPublisherRepository : IBaseRepository<Publisher>
    {
        Task<bool> IsPublisherExistingAsync(string publisherName);
        Task<int> GetIdByPublisherNameAsync(string publisherName);
    }
}