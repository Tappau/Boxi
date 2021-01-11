using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Boxi.Dal.Models
{
    public partial class ComicInventoryContext : DbContext
    {
        public ComicInventoryContext()
        {
        }

        public ComicInventoryContext(DbContextOptions<ComicInventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BoxStore> BoxStores { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<IssueCondition> IssueConditions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ComicInventory;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<BoxStore>(entity =>
            {
                entity.HasKey(e => e.BoxId)
                    .HasName("PK__BoxStore__136CF704405E414C");

                entity.ToTable("BoxStore");

                entity.Property(e => e.BoxId)
                    .ValueGeneratedNever()
                    .HasColumnName("BoxID");

                entity.Property(e => e.BoxName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QrData)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("QR_Data");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.AddressLine2).HasMaxLength(500);

                entity.Property(e => e.AddressLine3).HasMaxLength(500);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.PostcodeZip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Postcode_ZIP");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.GradeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GradeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("Issue");

                entity.HasIndex(e => e.Barcode, "idx_issue_barcode");

                entity.HasIndex(e => e.Isbn, "idx_issue_isbn");

                entity.HasIndex(e => e.Number, "idx_issue_number");

                entity.HasIndex(e => e.SeriesId, "idx_series_fk");

                entity.HasIndex(e => new { e.SeriesId, e.Number }, "unq_SeriesID_IssueNUmber")
                    .IsUnique();

                entity.Property(e => e.IssueId).HasColumnName("Issue_ID");

                entity.Property(e => e.AddedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(38)
                    .IsUnicode(false)
                    .HasColumnName("barcode");

                entity.Property(e => e.BoxId).HasColumnName("Box_ID");

                entity.Property(e => e.Editor)
                    .HasMaxLength(1600)
                    .IsUnicode(false)
                    .HasColumnName("editor");

                entity.Property(e => e.Frequency)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("frequency");

                entity.Property(e => e.GcdissueNumber).HasColumnName("GCDIssueNumber");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PageCount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("page_count");

                entity.Property(e => e.PublicationDate)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("publication_date");

                entity.Property(e => e.SeriesId).HasColumnName("Series_ID");

                entity.HasOne(d => d.Box)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.BoxId)
                    .HasConstraintName("fk_Issue_BoxStore");

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.SeriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Series_Issue");
            });

            modelBuilder.Entity<IssueCondition>(entity =>
            {
                entity.ToTable("IssueCondition");

                entity.HasIndex(e => new { e.IssueId, e.GradeId }, "unq_IssueConditon_GRADE_AND_ISSUE")
                    .IsUnique();

                entity.Property(e => e.IssueConditionId).HasColumnName("IssueCondition_ID");

                entity.Property(e => e.GradeId).HasColumnName("Grade_ID");

                entity.Property(e => e.IssueId).HasColumnName("Issue_ID");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.IssueConditions)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("fk_IssueCondition_Grade");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.IssueConditions)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("fk_IssueCondition_Issue");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.ShippedDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_Customers");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetail_ID");

                entity.Property(e => e.ItemId).HasColumnName("Item_ID");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("fk_OrderDetail_IssueCondition");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("fk_OrderDetail_Order");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonId).HasColumnName("Person_ID");

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.HasIndex(e => e.PubName, "idx_PublisherName");

                entity.Property(e => e.PublisherId).HasColumnName("Publisher_ID");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.PubName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Pub_Name");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.Property(e => e.YearBegan).HasColumnName("Year_Began");
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasIndex(e => e.SeriesName, "idx_SeriesName");

                entity.HasIndex(e => e.PublisherId, "idx_publisher_fk");

                entity.HasIndex(e => new { e.SeriesName, e.PublisherId }, "unq_SeriesName_PublisherID")
                    .IsUnique();

                entity.Property(e => e.SeriesId).HasColumnName("Series_ID");

                entity.Property(e => e.Dimensions)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("dimensions");

                entity.Property(e => e.PaperStock)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("paperStock");

                entity.Property(e => e.PublisherId).HasColumnName("Publisher_ID");

                entity.Property(e => e.SeriesName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Series_Name");

                entity.Property(e => e.YearBegan).HasColumnName("Year_Began");

                entity.Property(e => e.YearEnd).HasColumnName("Year_End");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Series)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_series_publisher");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
