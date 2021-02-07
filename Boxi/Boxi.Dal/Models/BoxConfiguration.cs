using Boxi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boxi.Dal.Models
{
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<Box> builder)
        {
            builder.ToTable("Box");
            builder.HasKey(e => e.Id)
                .HasName("PK_Box");

            builder.Property(e => e.Id)
                .HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(e => e.BoxName)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.QrData)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}