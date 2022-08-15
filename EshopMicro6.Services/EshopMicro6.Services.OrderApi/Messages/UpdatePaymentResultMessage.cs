using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.OrderApi.Messages
{
    public class UpdatePaymentResultMessage
    {
        public int OrderID { get; set; }
        public bool Status { get; set; }
    }
}