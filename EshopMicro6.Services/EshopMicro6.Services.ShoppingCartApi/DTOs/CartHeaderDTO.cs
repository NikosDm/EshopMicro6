using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.ShoppingCartApi.DTOs
{
    public class CartHeaderDTO
    {
        public int CartHeaderID { get; set; }
        public string UserID { get; set; }
        public string CouponCode { get; set; }
    }
}