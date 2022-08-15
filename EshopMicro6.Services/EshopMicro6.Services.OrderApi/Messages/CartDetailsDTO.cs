using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.OrderApi.Messages
{
    public class CartDetailsDTO
    {
        public int CartDetailsID { get; set; }
        public int CartHeaderID { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
        public int Count { get; set; }
    }
}