using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.ShoppingCartApi.Entities
{
    public class CartDetails
    {
        public int CartDetailsID { get; set; }
        public int CartHeaderID { get; set; }

        [ForeignKey("CartHeaderID")]
        public virtual CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}