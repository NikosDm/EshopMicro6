using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EshopMicro6.Services.ProductApi.DTOs;
using EshopMicro6.Services.ProductApi.Entities;

namespace EshopMicro6.Services.ProductApi.Helpers
{
    public class AutoMapperProfile
    {
        public static MapperConfiguration RegisterAutoMapper()
        {
            return new MapperConfiguration(config => {
               config.CreateMap<ProductDTO, Product>(); 
               config.CreateMap<Product, ProductDTO>(); 
            });
        }
    }
}