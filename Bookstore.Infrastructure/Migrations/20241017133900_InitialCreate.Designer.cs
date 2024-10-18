﻿// <auto-generated />
using System;
using Bookstore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookstore.Infrastructure.Migrations
{
    [DbContext(typeof(BookManagementDbContext))]
    [Migration("20241017133900_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("BookAuthor", b =>
                {
                    b.Property<string>("BookId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("book_id")
                        .IsFixedLength();

                    b.Property<string>("AuthorId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("author_id")
                        .IsFixedLength();

                    b.HasKey("BookId", "AuthorId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "AuthorId" }, "fk_ba_author");

                    b.ToTable("book_author", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Address", b =>
                {
                    b.Property<string>("AddressId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("address_id")
                        .IsFixedLength();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("city");

                    b.Property<string>("CountryId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("country_id")
                        .IsFixedLength();

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("street_name");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("street_number");

                    b.HasKey("AddressId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CountryId" }, "fk_addr_ctry");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.AddressStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("status_id");

                    b.Property<string>("AddressStatus1")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("address_status");

                    b.HasKey("StatusId")
                        .HasName("PRIMARY");

                    b.ToTable("address_status", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Author", b =>
                {
                    b.Property<string>("AuthorId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("author_id")
                        .IsFixedLength();

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasColumnName("author_name");

                    b.HasKey("AuthorId")
                        .HasName("PRIMARY");

                    b.ToTable("author", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Book", b =>
                {
                    b.Property<string>("BookId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("book_id")
                        .IsFixedLength();

                    b.Property<string>("Isbn13")
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("isbn13");

                    b.Property<string>("LanguageId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("language_id")
                        .IsFixedLength();

                    b.Property<int?>("NumPages")
                        .HasColumnType("int")
                        .HasColumnName("num_pages");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateOnly?>("PublicationDate")
                        .HasColumnType("date")
                        .HasColumnName("publication_date");

                    b.Property<string>("PublisherId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("publisher_id")
                        .IsFixedLength();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasColumnName("title");

                    b.HasKey("BookId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "LanguageId" }, "fk_book_lang");

                    b.HasIndex(new[] { "PublisherId" }, "fk_book_pub");

                    b.ToTable("book", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.BookLanguage", b =>
                {
                    b.Property<string>("LanguageId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("language_id")
                        .IsFixedLength();

                    b.Property<string>("LanguageCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)")
                        .HasColumnName("language_code");

                    b.Property<string>("LanguageName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("language_name");

                    b.HasKey("LanguageId")
                        .HasName("PRIMARY");

                    b.ToTable("book_language", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Country", b =>
                {
                    b.Property<string>("CountryId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("country_id")
                        .IsFixedLength();

                    b.Property<string>("CountryName")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("country_name");

                    b.HasKey("CountryId")
                        .HasName("PRIMARY");

                    b.ToTable("country", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.CustOrder", b =>
                {
                    b.Property<string>("OrderId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("order_id")
                        .IsFixedLength();

                    b.Property<string>("CustomerId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("customer_id")
                        .IsFixedLength();

                    b.Property<string>("DestAddressId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("dest_address_id")
                        .IsFixedLength();

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime")
                        .HasColumnName("order_date");

                    b.Property<string>("ShippingMethodId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("shipping_method_id")
                        .IsFixedLength();

                    b.HasKey("OrderId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "DestAddressId" }, "fk_order_addr");

                    b.HasIndex(new[] { "CustomerId" }, "fk_order_cust");

                    b.HasIndex(new[] { "ShippingMethodId" }, "fk_order_ship");

                    b.ToTable("cust_order", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("customer_id")
                        .IsFixedLength();

                    b.Property<string>("Email")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("last_name");

                    b.HasKey("CustomerId")
                        .HasName("PRIMARY");

                    b.ToTable("customer", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.CustomerAddress", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("customer_id")
                        .IsFixedLength();

                    b.Property<string>("AddressId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("address_id")
                        .IsFixedLength();

                    b.Property<int?>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("status_id");

                    b.HasKey("CustomerId", "AddressId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "AddressId" }, "fk_ca_addr");

                    b.ToTable("customer_address", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.OrderHistory", b =>
                {
                    b.Property<string>("HistoryId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("history_id")
                        .IsFixedLength();

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("order_id")
                        .IsFixedLength();

                    b.Property<DateTime>("StatusDate")
                        .HasColumnType("datetime")
                        .HasColumnName("status_date");

                    b.Property<int>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("status_id");

                    b.HasKey("HistoryId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "OrderId" }, "fk_oh_order");

                    b.HasIndex(new[] { "StatusId" }, "fk_oh_status");

                    b.ToTable("order_history", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.OrderLine", b =>
                {
                    b.Property<string>("LineId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("line_id")
                        .IsFixedLength();

                    b.Property<string>("BookId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("book_id")
                        .IsFixedLength();

                    b.Property<string>("OrderId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("order_id")
                        .IsFixedLength();

                    b.Property<decimal>("Price")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)")
                        .HasColumnName("price");

                    b.HasKey("LineId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "BookId" }, "fk_ol_book");

                    b.HasIndex(new[] { "OrderId" }, "fk_ol_order");

                    b.ToTable("order_line", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.OrderStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("status_id");

                    b.Property<string>("StatusValue")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status_value");

                    b.HasKey("StatusId")
                        .HasName("PRIMARY");

                    b.ToTable("order_status", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Publisher", b =>
                {
                    b.Property<string>("PublisherId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("publisher_id")
                        .IsFixedLength();

                    b.Property<string>("PublisherName")
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasColumnName("publisher_name");

                    b.HasKey("PublisherId")
                        .HasName("PRIMARY");

                    b.ToTable("publisher", (string)null);
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.ShippingMethod", b =>
                {
                    b.Property<string>("MethodId")
                        .HasMaxLength(32)
                        .HasColumnType("char(32)")
                        .HasColumnName("method_id")
                        .IsFixedLength();

                    b.Property<decimal?>("Cost")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)")
                        .HasColumnName("cost");

                    b.Property<string>("MethodName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("method_name");

                    b.HasKey("MethodId")
                        .HasName("PRIMARY");

                    b.ToTable("shipping_method", (string)null);
                });

            modelBuilder.Entity("BookAuthor", b =>
                {
                    b.HasOne("Bookstore.Domain.Entites.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("fk_ba_author");

                    b.HasOne("Bookstore.Domain.Entites.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("fk_ba_book");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Address", b =>
                {
                    b.HasOne("Bookstore.Domain.Entites.Country", "Country")
                        .WithMany("Addresses")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("fk_addr_ctry");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Book", b =>
                {
                    b.HasOne("Bookstore.Domain.Entites.BookLanguage", "Language")
                        .WithMany("Books")
                        .HasForeignKey("LanguageId")
                        .HasConstraintName("fk_book_lang");

                    b.HasOne("Bookstore.Domain.Entites.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .HasConstraintName("fk_book_pub");

                    b.Navigation("Language");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.CustOrder", b =>
                {
                    b.HasOne("Bookstore.Domain.Entites.Customer", "Customer")
                        .WithMany("CustOrders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("fk_order_cust");

                    b.HasOne("Bookstore.Domain.Entites.Address", "DestAddress")
                        .WithMany("CustOrders")
                        .HasForeignKey("DestAddressId")
                        .HasConstraintName("fk_order_addr");

                    b.HasOne("Bookstore.Domain.Entites.ShippingMethod", "ShippingMethod")
                        .WithMany("CustOrders")
                        .HasForeignKey("ShippingMethodId")
                        .HasConstraintName("fk_order_ship");

                    b.Navigation("Customer");

                    b.Navigation("DestAddress");

                    b.Navigation("ShippingMethod");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.CustomerAddress", b =>
                {
                    b.HasOne("Bookstore.Domain.Entites.Address", "Address")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("AddressId")
                        .IsRequired()
                        .HasConstraintName("fk_ca_addr");

                    b.HasOne("Bookstore.Domain.Entites.Customer", "Customer")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("fk_ca_cust");

                    b.Navigation("Address");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.OrderHistory", b =>
                {
                    b.HasOne("Bookstore.Domain.Entites.CustOrder", "Order")
                        .WithMany("OrderHistories")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_oh_order");

                    b.HasOne("Bookstore.Domain.Entites.OrderStatus", "Status")
                        .WithMany("OrderHistories")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_oh_status");

                    b.Navigation("Order");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.OrderLine", b =>
                {
                    b.HasOne("Bookstore.Domain.Entites.Book", "Book")
                        .WithMany("OrderLines")
                        .HasForeignKey("BookId")
                        .HasConstraintName("fk_ol_book");

                    b.HasOne("Bookstore.Domain.Entites.CustOrder", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("fk_ol_order");

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Address", b =>
                {
                    b.Navigation("CustOrders");

                    b.Navigation("CustomerAddresses");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Book", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.BookLanguage", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Country", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.CustOrder", b =>
                {
                    b.Navigation("OrderHistories");

                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Customer", b =>
                {
                    b.Navigation("CustOrders");

                    b.Navigation("CustomerAddresses");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.OrderStatus", b =>
                {
                    b.Navigation("OrderHistories");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Bookstore.Domain.Entites.ShippingMethod", b =>
                {
                    b.Navigation("CustOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
