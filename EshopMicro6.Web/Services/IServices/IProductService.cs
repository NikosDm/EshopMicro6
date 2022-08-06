using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;

namespace EshopMicro6.Web.Services.IServices;

public interface IProductService : IBaseService
{
    Task<T> GetAllProductsAsync<T>(string token);
    Task<T> GetProductByIDAsync<T>(string token, int id);
    Task<T> CreateProductAsync<T>(string token, ProductDTO productDTO);
    Task<T> UpdateProductAsync<T>(string token, ProductDTO productDTO);
    Task<T> DeleteProductAsync<T>(string token, int id);
}
