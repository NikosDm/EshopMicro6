using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EshopMicro6.Services.ShoppingCartApi.DTOs;
using EshopMicro6.Services.ShoppingCartApi.Entities;

namespace EshopMicro6.Services.ShoppingCartApi.Helpers;

public class AutoMapperProfile
{
    public static MapperConfiguration RegisterAutoMapper()
    {
        return new MapperConfiguration(config => {
            config.CreateMap<ProductDTO, Product>().ReverseMap(); 
            config.CreateMap<CartHeader, CartHeaderDTO>().ReverseMap(); 
            config.CreateMap<CartDetails, CartDetailsDTO>().ReverseMap(); 
            config.CreateMap<Cart, CartDTO>().ReverseMap(); 
        });
    }
}
