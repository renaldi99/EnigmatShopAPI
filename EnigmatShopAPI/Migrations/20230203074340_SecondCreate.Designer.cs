﻿// <auto-generated />
using System;
using EnigmatShopAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnigmatShopAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230203074340_SecondCreate")]
    partial class SecondCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EnigmatShopAPI.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("customer_name");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("NomorHp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nomor_hp");

                    b.HasKey("Id");

                    b.ToTable("ES_Customer_TM");
                });

            modelBuilder.Entity("EnigmatShopAPI.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("product_name");

                    b.Property<string>("ProductPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("product_price");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.HasKey("Id");

                    b.ToTable("ES_Product_TM");
                });

            modelBuilder.Entity("EnigmatShopAPI.Models.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("date");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("ES_Purchase_TM");
                });

            modelBuilder.Entity("EnigmatShopAPI.Models.PurchaseDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<Guid>("PurchaseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("purchase_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("ES_PurchaseDetail_TM");
                });

            modelBuilder.Entity("EnigmatShopAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVarchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("NVarchar(150)")
                        .HasColumnName("password");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("refresh_token");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("NVarchar(10)")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("NVarchar(50)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("ES_User_TM");
                });

            modelBuilder.Entity("EnigmatShopAPI.Models.Purchase", b =>
                {
                    b.HasOne("EnigmatShopAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("EnigmatShopAPI.Models.PurchaseDetail", b =>
                {
                    b.HasOne("EnigmatShopAPI.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnigmatShopAPI.Models.Purchase", "Purchase")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("EnigmatShopAPI.Models.Purchase", b =>
                {
                    b.Navigation("PurchaseDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
