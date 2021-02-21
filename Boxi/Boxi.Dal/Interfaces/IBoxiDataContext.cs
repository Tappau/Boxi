using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Boxi.Dal.Models
{
    public interface IBoxiDataContext
    {
        DbSet<Box> Box { get; set; }
        DbSet<Item> Items { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}