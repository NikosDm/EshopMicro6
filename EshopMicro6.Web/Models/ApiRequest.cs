using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EshopMicro6.Web.SD;

namespace EshopMicro6.Web.Models;

public class ApiRequest
{
       public ApiType ApiType { get; set; } = ApiType.Get;
       public string Url { get; set; }
       public object Data { get; set; }
       public string Token { get; set; }
}
