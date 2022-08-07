using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.ShoppingCartApi.Entities;

namespace EshopMicro6.Services.ShoppingCartApi.DTOs
{
    public class CartDetailsDTO
    {
        public int CartDetailsID { get; set; }
        public int CartHeaderID { get; set; }
        public virtual CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}