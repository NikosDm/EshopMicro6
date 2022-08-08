using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EshopMicro6.Services.CouponApi.Data;
using EshopMicro6.Services.CouponApi.DTOs;
using EshopMicro6.Services.CouponApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.CouponApi.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _dbContext; 
        private IMapper _mapper;

        public CouponRepository(AppDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<CouponDTO> GetCouponByCode(string couponCode)
        {
            var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(u => u.CouponCode == couponCode);

            return _mapper.Map<CouponDTO>(coupon);
        }
    }
}