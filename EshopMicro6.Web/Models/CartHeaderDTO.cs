using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Web.Models
{
    public class CartHeaderDTO
    {
        public int CartHeaderID { get; set; }
        public string UserID { get; set; }
        public string CouponCode { get; set; }
        public double OrderTotal { get; set; }
    }
}