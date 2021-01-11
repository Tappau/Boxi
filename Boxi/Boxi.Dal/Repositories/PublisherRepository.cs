using System;
using System.Linq;
using System.Threading.Tasks;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Boxi.Dal.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(ComicInventoryContext context) : base(context)
        {
        }

        public async Task<bool> IsPublisherExistingAsync(string publisherName)
        {
            IsPublisherNameValid(publisherName);
            return await ExistsAsync(publisher =>
                publisher.PubName.Equals(publisherName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<int> GetIdByPublisherNameAsync(string publisherName)
        {
            IsPublisherNameValid(publisherName);

            return await Context.Publishers
                .Where(x => x.PubName.Equals(publisherName, StringComparison.OrdinalIgnoreCase))
                .Select(p => p.PublisherId).FirstOrDefaultAsync();
        }

        private void IsPublisherNameValid(string publisherName)
        {
            if (string.IsNullOrWhiteSpace(publisherName))
            {
                throw new ArgumentException("Publisher Name is invalid, empty or white space", nameof(publisherName));
            }
        }
    }
}