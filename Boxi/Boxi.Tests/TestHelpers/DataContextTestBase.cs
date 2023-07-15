using System.Collections.Generic;
using System.Data.Common;
using Boxi.Core.Domain;
using Boxi.Dal.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Boxi.Tests.TestHelpers
{
    public abstract class DataContextTestBase
    {
        protected DataContextTestBase(DbContextOptions<BoxiDataContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Boxes = new List<Box>();
            Items = new List<Item>();

            InternalSeed();
        }

        private void InternalSeed()
        {
            using (var context = new BoxiDataContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Boxes = DefineBoxes() ?? new List<Box>();
                Items = DefineItems() ?? new List<Item>();

                context.AddRange(Boxes);
                context.AddRange(Items);
                context.SaveChanges();
            }
        }

        protected virtual List<Item> DefineItems()
        {
            return null;
        }

        protected virtual List<Box> DefineBoxes()
        {
            return null;
        }

        protected DbContextOptions<BoxiDataContext> ContextOptions { get; set; }

        public List<Box> Boxes { get; set; }

        public List<Item> Items { get; set; }

        protected static DbConnection AsInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }
    }
}
