using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.OrderApi.Entities;

namespace EshopMicro6.Services.OrderApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(int orderHeaderID, bool paymentStatus);
    }
}