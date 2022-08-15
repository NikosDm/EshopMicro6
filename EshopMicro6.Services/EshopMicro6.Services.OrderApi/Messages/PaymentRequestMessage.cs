using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Integration.MessageBus;

namespace EshopMicro6.Services.OrderApi.Messages
{
    public class PaymentRequestMessage : BaseMessage
    {
        public int OrderID { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpirtyMonthYear { get; set; }
        public double OrderTotal { get; set; }
    }
}