using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Web;

public static class SD
{
    public static string ProductAPIBase { get; set; }
    public static string ShoppingCartAPIBase { get; set; }
    public static string CouponAPIBase { get; set; }

    public enum ApiType 
    {
        Get,
        Post,
        Put,
        Delete
    }
}
