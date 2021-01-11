using System.Collections.Generic;
using System.Threading.Tasks;
using Boxi.Core.DTO;
using Boxi.Dal.Models;

namespace Boxi.Dal.Interfaces
{
    public interface IBoxStoreRepository : IBaseRepository<BoxStore>
    {
        /// <summary>
        ///     Retires the specified box identifier.
        /// Requires to be saved by <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="boxId">The box identifier.</param>
        void RetireBox(int boxId);
        /// <summary>
        ///     Retires the specified box identifier. Async
        /// Requires to be saved by <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="boxId">The box identifier.</param>
        Task RetireBoxAsync(int boxId);

        /// <summary>
        ///     Gets highest BoxID and adds one to work out what the next box number will be
        ///     This is purely for displaying of information during creation of a new box
        /// Requires to be saved by <see cref="IUnitOfWork"/>
        /// </summary>
        /// <returns></returns>
        Task<int> FetchNextBoxIdAsync();

        Task<IEnumerable<BoxContentDto>> GetBoxContents(int boxId);
    }
}