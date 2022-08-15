using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.OrderApi.Data;
using EshopMicro6.Services.OrderApi.Entities;
using EshopMicro6.Services.OrderApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.OrderApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<AppDbContext> _dbContext; 
        
        public OrderRepository(DbContextOptions<AppDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            await using var _db = new AppDbContext(_dbContext);

            await _db.OrderHeaders.AddAsync(orderHeader);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderID, bool paymentStatus)
        {
            await using var _db = new AppDbContext(_dbContext);

            var orderHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.OrderHeaderID == orderHeaderID);

            if (orderHeader != null)
            {
                orderHeader.PaymentStatus = paymentStatus;
                _db.OrderHeaders.Update(orderHeader);

                await _db.SaveChangesAsync();
            }
        }
    }
}