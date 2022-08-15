using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.PaymentApi.Interfaces
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}