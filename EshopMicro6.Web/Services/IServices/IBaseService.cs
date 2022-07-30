using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;

namespace EshopMicro6.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseDTO responseDTO { get; set; }
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}