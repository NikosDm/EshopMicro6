using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.EmailApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.EmailApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EmailLog> EmailLogs { get; set; }
}

