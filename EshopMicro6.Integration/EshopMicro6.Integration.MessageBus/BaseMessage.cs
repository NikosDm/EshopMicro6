using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Integration.MessageBus
{
    public class BaseMessage
    {
        public int Id { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}