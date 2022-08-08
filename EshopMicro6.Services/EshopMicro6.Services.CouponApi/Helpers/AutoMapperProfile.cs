using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EshopMicro6.Services.CouponApi.DTOs;
using EshopMicro6.Services.CouponApi.Entities;

namespace EshopMicro6.Services.CouponApi.Helpers;

public class AutoMapperProfile
{
    public static MapperConfiguration RegisterAutoMapper()
    {
        return new MapperConfiguration(config => {
            config.CreateMap<CouponDTO, Coupon>().ReverseMap(); 
        });
    }
}
