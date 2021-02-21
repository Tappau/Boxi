using Boxi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boxi.Dal.Models
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasIndex(e => e.Barcode, "idx_issue_barcode");
            builder.HasKey(item => item.Id).HasName("PK_Item");
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Barcode)
                .HasMaxLength(38)
                .IsUnicode(false);
        }
    }
}