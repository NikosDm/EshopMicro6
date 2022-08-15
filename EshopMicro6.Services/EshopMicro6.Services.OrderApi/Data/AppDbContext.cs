using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.OrderApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.OrderApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
}

