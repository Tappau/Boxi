using System;
using System.Data.Common;
using Boxi.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Boxi.Tests
{
    public class SqliteInMemoryDataContext : DataContextTestBase, IDisposable
    {
        private readonly DbConnection _connection;
        public SqliteInMemoryDataContext() : base(
            new DbContextOptionsBuilder<BoxiDataContext>()
                .UseSqlite(AsInMemoryDatabase())
                .Options)
        {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}