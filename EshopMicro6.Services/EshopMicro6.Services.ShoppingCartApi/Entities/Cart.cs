using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.ShoppingCartApi.Entities
{
    public class Cart
    {
        public CartHeader cartHeader { get; set; }
        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}