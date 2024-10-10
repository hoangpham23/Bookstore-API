using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Bookstore.Infrastructure;

public partial class BookManagementDbContext : DbContext
{
    public BookManagementDbContext()
    {
    }

    public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressStatus> AddressStatuses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookLanguage> BookLanguages { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CustOrder> CustOrders { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("address");

            entity.HasIndex(e => e.CountryId, "fk_addr_ctry");

            entity.Property(e => e.AddressId)
                .ValueGeneratedOnAdd()
                .HasColumnName("address_id");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.StreetName)
                .HasMaxLength(200)
                .HasColumnName("street_name");
            entity.Property(e => e.StreetNumber)
                .HasMaxLength(10)
                .HasColumnName("street_number");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("fk_addr_ctry");
        });

        modelBuilder.Entity<AddressStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("address_status");

            entity.Property(e => e.StatusId)
                .ValueGeneratedOnAdd()
                .HasColumnName("status_id");
            entity.Property(e => e.AddressStatus1)
                .HasMaxLength(30)
                .HasColumnName("address_status");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");

            entity.ToTable("author");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedOnAdd()
                .HasColumnName("author_id");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(400)
                .HasColumnName("author_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("book");

            entity.HasIndex(e => e.LanguageId, "fk_book_lang");

            entity.HasIndex(e => e.PublisherId, "fk_book_pub");

            entity.Property(e => e.BookId)
                .ValueGeneratedOnAdd()
                .HasColumnName("book_id");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("isbn13");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.NumPages).HasColumnName("num_pages");
            entity.Property(e => e.PublicationDate).HasColumnName("publication_date");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.Title)
                .HasMaxLength(400)
                .HasColumnName("title");

            entity.HasOne(d => d.Language).WithMany(p => p.Books)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("fk_book_lang");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("fk_book_pub");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ba_author"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ba_book"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("book_author");
                        j.HasIndex(new[] { "AuthorId" }, "fk_ba_author");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("author_id");
                    });
        });

        modelBuilder.Entity<BookLanguage>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PRIMARY");

            entity.ToTable("book_language");

            entity.Property(e => e.LanguageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("language_id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(8)
                .HasColumnName("language_code");
            entity.Property(e => e.LanguageName)
                .HasMaxLength(50)
                .HasColumnName("language_name");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.ToTable("country");

            entity.Property(e => e.CountryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(200)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<CustOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("cust_order");

            entity.HasIndex(e => e.DestAddressId, "fk_order_addr");

            entity.HasIndex(e => e.CustomerId, "fk_order_cust");

            entity.HasIndex(e => e.ShippingMethodId, "fk_order_ship");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DestAddressId).HasColumnName("dest_address_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.ShippingMethodId).HasColumnName("shipping_method_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("fk_order_cust");

            entity.HasOne(d => d.DestAddress).WithMany(p => p.CustOrders)
                .HasForeignKey(d => d.DestAddressId)
                .HasConstraintName("fk_order_addr");

            entity.HasOne(d => d.ShippingMethod).WithMany(p => p.CustOrders)
                .HasForeignKey(d => d.ShippingMethodId)
                .HasConstraintName("fk_order_ship");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("customer_id");
            entity.Property(e => e.Email)
                .HasMaxLength(350)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.AddressId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("customer_address");

            entity.HasIndex(e => e.AddressId, "fk_ca_addr");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Address).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ca_addr");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ca_cust");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PRIMARY");

            entity.ToTable("order_history");

            entity.HasIndex(e => e.OrderId, "fk_oh_order");

            entity.HasIndex(e => e.StatusId, "fk_oh_status");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.StatusDate)
                .HasColumnType("datetime")
                .HasColumnName("status_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_oh_order");

            entity.HasOne(d => d.Status).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_oh_status");
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PRIMARY");

            entity.ToTable("order_line");

            entity.HasIndex(e => e.BookId, "fk_ol_book");

            entity.HasIndex(e => e.OrderId, "fk_ol_order");

            entity.Property(e => e.LineId).HasColumnName("line_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasPrecision(5, 2)
                .HasColumnName("price");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderLines)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("fk_ol_book");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderLines)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_ol_order");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("order_status");

            entity.Property(e => e.StatusId)
                .ValueGeneratedOnAdd()
                .HasColumnName("status_id");
            entity.Property(e => e.StatusValue)
                .HasMaxLength(20)
                .HasColumnName("status_value");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PRIMARY");

            entity.ToTable("publisher");

            entity.Property(e => e.PublisherId)
                .ValueGeneratedOnAdd()
                .HasColumnName("publisher_id");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(400)
                .HasColumnName("publisher_name");
        });

        modelBuilder.Entity<ShippingMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PRIMARY");

            entity.ToTable("shipping_method");

            entity.Property(e => e.MethodId)
                .ValueGeneratedOnAdd()
                .HasColumnName("method_id");
            entity.Property(e => e.Cost)
                .HasPrecision(6, 2)
                .HasColumnName("cost");
            entity.Property(e => e.MethodName)
                .HasMaxLength(100)
                .HasColumnName("method_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
