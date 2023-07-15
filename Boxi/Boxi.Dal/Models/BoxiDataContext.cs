using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Boxi.Dal.Models
{
    public partial class BoxiDataContext : DbContext
    {
        public BoxiDataContext()
        {
        }

        public BoxiDataContext(DbContextOptions<BoxiDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Box> Box { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxiDataContext).Assembly);

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    //Add Shadow property of CreatedOn and ModifiedOn to all entities
            //    //entityType.AddProperty("CreatedOn", typeof(DateTime));
            //   // entityType.AddProperty("ModifiedOn", typeof(DateTime));
            //}

            OnModelCreatingPartial(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetCreatedModifiedOnValues();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetCreatedModifiedOnValues();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetCreatedModifiedOnValues()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        ((BaseEntity)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
                        ((BaseEntity)entityEntry.Entity).ModifiedOn = null;
                        break;
                    case EntityState.Modified:
                        ((BaseEntity)entityEntry.Entity).ModifiedOn = DateTime.UtcNow;
                        break;
                }
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
