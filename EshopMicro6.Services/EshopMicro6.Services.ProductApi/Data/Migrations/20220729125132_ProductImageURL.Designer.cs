﻿// <auto-generated />
using EshopMicro6.Services.ProductApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EshopMicro6.Services.ProductApi.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220729125132_ProductImageURL")]
    partial class ProductImageURL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EshopMicro6.Services.ProductApi.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Appetizer",
                            Description = "Details about the product",
                            ImageUrl = "https://nthomadakis.blob.core.windows.net/eshop/14.jpg",
                            Name = "Samosa",
                            Price = 15.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Appetizer",
                            Description = "Details about the product 2",
                            ImageUrl = "https://nthomadakis.blob.core.windows.net/eshop/12.jpg",
                            Name = "Mango",
                            Price = 13.99
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Dessert",
                            Description = "Details about the product 3",
                            ImageUrl = "https://nthomadakis.blob.core.windows.net/eshop/11.jpg",
                            Name = "Sweet Pie",
                            Price = 10.99
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Dessert",
                            Description = "Details about the product 4",
                            ImageUrl = "https://nthomadakis.blob.core.windows.net/eshop/13.jpg",
                            Name = "Pav Bhaji",
                            Price = 10.99
                        });
                });
#pragma warning restore 612, 618
        }
    }
}