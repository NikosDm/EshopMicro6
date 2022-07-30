using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;
using EshopMicro6.Web.Services.IServices;

namespace EshopMicro6.Web.Services;

public class ProductService : BaseService, IProductService
{
    private readonly IHttpClientFactory _clientFactory;

    public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<T> CreateProductAsync<T>(ProductDTO productDTO)
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Post,
            Data = productDTO,
            Url = SD.ProductAPIBase + "api/products",
            Token = ""
        });
    }

    public async Task<T> DeleteProductAsync<T>(int id)
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Delete,
            Url = SD.ProductAPIBase + "api/products/" + id,
            Token = ""
        });
    }

    public async Task<T> GetAllProductsAsync<T>()
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ProductAPIBase + "api/products/",
            Token = ""
        });
    }

    public async Task<T> GetProductByIDAsync<T>(int id)
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Get,
            Url = SD.ProductAPIBase + "api/products/" + id,
            Token = ""
        });
    }

    public async Task<T> UpdateProductAsync<T>(ProductDTO productDTO)
    {
        return await this.SendAsync<T>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Put,
            Data = productDTO,
            Url = SD.ProductAPIBase + "api/products",
            Token = ""
        });
    }
}
