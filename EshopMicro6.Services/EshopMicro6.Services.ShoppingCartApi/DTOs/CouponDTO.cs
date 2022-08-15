using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.ShoppingCartApi.DTOs;

public class CouponDTO
{
    public int CouponID { get; set; }   
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
}