using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.CouponApi.DTOs;

namespace EshopMicro6.Services.CouponApi.Interfaces;

public interface ICouponRepository
{
    Task<CouponDTO> GetCouponByCode(string couponCode);
}
