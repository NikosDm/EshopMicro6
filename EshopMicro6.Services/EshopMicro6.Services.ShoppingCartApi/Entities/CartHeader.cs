using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.ShoppingCartApi.Entities
{
    public class CartHeader
    {
        [Key]
        public int CartHeaderID { get; set; }
        public string UserID { get; set; }
        public string CouponCode { get; set; }
    }
}