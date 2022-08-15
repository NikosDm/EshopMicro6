using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.OrderApi.Entities
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsID { get; set; }
        public int OrderHeaderID { get; set; }

        [ForeignKey("OrderHeaderID")]
        public virtual OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}