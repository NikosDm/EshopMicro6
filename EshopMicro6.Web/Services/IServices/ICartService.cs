using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;

namespace EshopMicro6.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIDAsync<T>(string userID, string token = null);
        Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null);
        Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null);
        Task<T> RemoveFromCartAsync<T>(int cardID, string token = null);
    }
}