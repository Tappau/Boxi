using System.Threading.Tasks;
using Boxi.Core.Domain;

namespace Boxi.Dal.Interfaces
{
    public interface IBoxRepository : IBaseRepository<Box>
    {
        /// <summary>
        ///     Gets highest BoxID and adds one to work out what the next box number will be
        ///     This is purely for displaying of information during creation of a new box
        ///     Requires to be saved by <see cref="IUnitOfWork" />
        /// </summary>
        /// <returns></returns>
        Task<int> FetchNextBoxIdAsync();
    }
}