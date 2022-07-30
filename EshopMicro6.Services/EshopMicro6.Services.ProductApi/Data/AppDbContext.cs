using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.ProductApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.ProductApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(new Product 
        {
            ProductId = 1,
            Name = "Samosa",
            Price = 15,
            Description = "Details about the product",
            ImageUrl = "https://nthomadakis.blob.core.windows.net/eshop/14.jpg",
            CategoryName = "Appetizer"
        });

        modelBuilder.Entity<Product>().HasData(new Product 
        {
            ProductId = 2,
            Name = "Mango",
            Price = 13.99,
            Description = "Details about the product 2",
            ImageUrl = "https://nthomadakis.blob.core.windows.net/eshop/12.jpg",
            CategoryName = "Appetizer"
        });

        modelBuilder.Entity<Product>().HasData(new Product 
        {
            ProductId = 3,
            Name = "Sweet Pie",
            Price = 10.99,
            Description = "Details about the product 3",
            ImageUrl = "https://nthomadakis.blob.core.windows.net/eshop/11.jpg",
            CategoryName = "Dessert"
        });

        modelBuilder.Entity<Product>().HasData(new Product 
        {
            ProductId = 4,
            Name = "Pav Bhaji",
            Price = 10.99,
            Description = "Details about the product 4",
            ImageUrl = "https://nthomadakis.blob.core.windows.net/eshop/13.jpg",
            CategoryName = "Dessert"
        });
    }
}

