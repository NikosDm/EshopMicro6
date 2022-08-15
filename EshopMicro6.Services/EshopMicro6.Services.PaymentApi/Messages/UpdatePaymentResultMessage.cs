using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Integration.MessageBus;

namespace EshopMicro6.Services.PaymentApi.Messages
{
    public class UpdatePaymentResultMessage : BaseMessage
    {
        public int OrderID { get; set; }
        public bool Status { get; set; }
    }
}