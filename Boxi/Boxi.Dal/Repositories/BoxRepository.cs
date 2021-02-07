using System.Threading.Tasks;
using Boxi.Core.Domain;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Boxi.Dal.Repositories
{
    public class BoxRepository : BaseRepository<Box>, IBoxRepository
    {
        public BoxRepository(BoxiDataContext context) : base(context)
        {
            
        }

        /// <summary>
        ///     Retires the specified box identifier. Async
        /// Requires to be saved by <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="boxId">The box identifier.</param>
        public async Task RetireBoxAsync(int boxId)
        {
            var box = await GetAsync(boxId);
            box.IsDeleted = true;
            Update(box);
        }

        /// <summary>
        ///     Gets highest BoxID and adds one to work out what the next box number will be
        ///     This is purely for displaying of information during creation of a new box
        /// Requires to be saved by <see cref="IUnitOfWork"/>
        /// </summary>
        /// <returns></returns>
        public async Task<int> FetchNextBoxIdAsync()
        {
            return await Context.Box.MaxAsync(x => x.Id) + 1;
        }
    }
}