using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.ShoppingCartApi.DTOs;

namespace EshopMicro6.Services.ShoppingCartApi.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponName);
    }
}