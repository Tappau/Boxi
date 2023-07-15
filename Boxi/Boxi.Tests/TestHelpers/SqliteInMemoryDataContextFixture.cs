using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Boxi.Core.Domain;
using Boxi.Dal;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Boxi.Tests.TestHelpers
{
    public class SqliteInMemoryDataContextFixture : DataContextTestBase, IDisposable
    {
        private readonly DbConnection _connection;

        public IUnitOfWork UoW { get; private set; }
        public SqliteInMemoryDataContextFixture() : base(
            new DbContextOptionsBuilder<BoxiDataContext>()
                .UseSqlite(AsInMemoryDatabase())
                .Options)
        {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
            UoW = new UnitOfWork(new BoxiDataContext(ContextOptions));
        }

        protected override List<Box> DefineBoxes()
        {
            var boxData = new List<Box>();
            for (var i = 1; i <= 3; i++)
            {
                boxData.Add(new Box($"Box {i}", $"Notes for Box {i}")
                {
                    Items = new List<Item>()
                {
                    new Item($"Box {i}, Item {i}"),
                    new Item($"Box {i}, Item {i + 1}")
                }
                });
            }

            boxData.Last().Items.Add(new Item("Item has barcode", "BARCODE"));
            boxData.First().Items.Add(new Item("Item has barcode", "BARCODE"));

            return boxData;
        }

        public void Dispose()
        {
            ContextOptions = null;
            _connection?.Dispose();
        }
    }
}