using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;
using EshopMicro6.Web.Services.IServices;

namespace EshopMicro6.Web.Services;

public class CartService : BaseService, ICartService
{
    private readonly IHttpClientFactory _clientFactory;

    public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Post,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
            Token = token
        });
    }

    public async Task<T> GetCartByUserIDAsync<T>(string userID, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userID,
            Token = token
        });
    }

    public async Task<T> RemoveFromCartAsync<T>(int cardID, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Post,
            Data = cardID,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
            Token = token
        });
    }
    
    public async Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Post,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart",
            Token = token
        });
    }
}
