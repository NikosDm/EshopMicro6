using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.ShoppingCartApi.Entities;

namespace EshopMicro6.Services.ShoppingCartApi.DTOs
{
    public class CartDTO
    {
        public CartHeader cartHeader { get; set; }
        public IEnumerable<CartDetailsDTO> CartDetails { get; set; }
    }
}