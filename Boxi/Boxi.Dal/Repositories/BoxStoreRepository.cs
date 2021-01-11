using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Boxi.Core.DTO;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Boxi.Dal.Repositories
{
    public class BoxStoreRepository : BaseRepository<BoxStore>, IBoxStoreRepository
    {
        private readonly IDbConnection _dapperConnection;

        public BoxStoreRepository(ComicInventoryContext context, IDbConnection dapperConnection) : base(context)
        {
            _dapperConnection = dapperConnection;
        }

        /// <summary>
        ///     Retires the specified box identifier.
        /// Requires to be saved by <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="boxId">The box identifier.</param>
        public void RetireBox(int boxId)
        {
            var box = this.Get(boxId);
            box.IsActive = false;
            Update(box);
        }

        /// <summary>
        ///     Retires the specified box identifier. Async
        /// Requires to be saved by <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="boxId">The box identifier.</param>
        public async Task RetireBoxAsync(int boxId)
        {
            var box = await this.GetAsync(boxId);
            box.IsActive = false;
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
            var highestBoxId = await Context.BoxStores.MaxAsync(x => x.BoxId);
            return highestBoxId + 1;
        }

        public async Task<IEnumerable<BoxContentDto>> GetBoxContents(int boxId)
        {
            const string sql = "SELECT i.Issue_ID AS IssueId," +
                               "p.Pub_Name AS Publisher," +
                               "s.Series_Name AS SeriesName," +
                               "i.Number AS IssueNumber," +
                               "issuecon.quantity AS Qty," +
                               "con.Name AS Condition," +
                               "issueCon.Grade_ID AS GradeId " +
                               "FROM Issue i " +
                               "INNER JOIN Series s ON i.Series_ID = s.Series_ID " +
                               "INNER JOIN Publisher p ON s.Publisher_ID = p.Publisher_ID " +
                               "INNER JOIN IssueCondition issueCon ON i.Issue_ID = issueCon.Issue_ID " +
                               "INNER JOIN Grade con ON issueCon.Grade_ID = con.GradeID " +
                               "INNER JOIN BoxStore b ON i.Box_ID = b.BoxID " +
                               "WHERE b.BoxID = @id " +
                               "ORDER BY i.Issue_ID ASC";

            return await _dapperConnection.QueryAsync<BoxContentDto>(sql, new {id = boxId});
        }

        protected override void Dispose(bool disposing)
        {
            _dapperConnection.Dispose();
            base.Dispose(disposing);
        }
    }

    
}