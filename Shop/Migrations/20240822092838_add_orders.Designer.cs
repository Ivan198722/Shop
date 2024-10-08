﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop;

#nullable disable

namespace Shop.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240822092838_add_orders")]
    partial class add_orders
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Shop.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NumberFlat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberHouse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postcode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Shop.Models.FinishedOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("orderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("orderTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("orderId");

                    b.ToTable("FinishedOrders");
                });

            modelBuilder.Entity("Shop.Models.Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("img")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("numberImgs")
                        .HasColumnType("int");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Shop.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("customerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("orderTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("paymentStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("customerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Shop.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("orderId")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("productID")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("orderId");

                    b.HasIndex("productID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("addDate")
                        .HasColumnType("date");

                    b.Property<bool>("available")
                        .HasColumnType("bit");

                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.Property<bool>("isFavorited")
                        .HasColumnType("bit");

                    b.Property<string>("longDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<string>("shortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("categoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shop.Models.ProductHighlights", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.ToTable("ProductHighlights");
                });

            modelBuilder.Entity("Shop.Models.ProductProperties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<string>("propertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propertyParameters")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("categoryId");

                    b.HasIndex("productId");

                    b.ToTable("ProductProperties");
                });

            modelBuilder.Entity("Shop.Models.ShopCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ShopCartId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("imagesId")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("pricePerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("imagesId");

                    b.HasIndex("productId");

                    b.ToTable("ShopCartItems");
                });

            modelBuilder.Entity("Shop.Models.FinishedOrder", b =>
                {
                    b.HasOne("Shop.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Shop.Models.Images", b =>
                {
                    b.HasOne("Shop.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Models.Order", b =>
                {
                    b.HasOne("Shop.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Shop.Models.OrderDetail", b =>
                {
                    b.HasOne("Shop.Models.Order", "Order")
                        .WithMany("orderDetails")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("productID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.HasOne("Shop.Models.Category", "Category")
                        .WithMany("products")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Shop.Models.ProductHighlights", b =>
                {
                    b.HasOne("Shop.Models.Product", "Product")
                        .WithMany("Highlights")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Models.ProductProperties", b =>
                {
                    b.HasOne("Shop.Models.Category", "Category")
                        .WithMany("productProperties")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shop.Models.Product", "Product")
                        .WithMany("ProductProperties")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Models.ShopCartItem", b =>
                {
                    b.HasOne("Shop.Models.Images", "images")
                        .WithMany()
                        .HasForeignKey("imagesId");

                    b.HasOne("Shop.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("images");

                    b.Navigation("product");
                });

            modelBuilder.Entity("Shop.Models.Category", b =>
                {
                    b.Navigation("productProperties");

                    b.Navigation("products");
                });

            modelBuilder.Entity("Shop.Models.Order", b =>
                {
                    b.Navigation("orderDetails");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.Navigation("Highlights");

                    b.Navigation("Images");

                    b.Navigation("ProductProperties");
                });
#pragma warning restore 612, 618
        }
    }
}
