using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Payments.PaymentProcessor
{
    public interface IProcessPayment
    {
        bool PaymentProcessor();
    }
}