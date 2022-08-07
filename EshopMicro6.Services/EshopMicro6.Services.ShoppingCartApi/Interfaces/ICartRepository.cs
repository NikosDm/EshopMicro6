using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.ShoppingCartApi.DTOs;

namespace EshopMicro6.Services.ShoppingCartApi.Interfaces;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserID(string UserID);
    Task<CartDTO> UpsertCart(CartDTO cartDTO);
    Task<bool> RemoveFromCart(int CartDetailsID);
    Task<bool> ClearShoppingCart(string UserID);
}
