using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.CouponApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.CouponApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(new Coupon 
        {
            CouponID = 1,
            CouponCode = "10OFF",
            DiscountAmount = 10
        });

        modelBuilder.Entity<Coupon>().HasData(new Coupon 
        {
            CouponID = 2,
            CouponCode = "20OFF",
            DiscountAmount = 20
        });
    }
}

