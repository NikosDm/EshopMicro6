using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;

namespace EshopMicro6.Web.Services.IServices;

public interface IProductService : IBaseService
{
    Task<T> GetAllProductsAsync<T>();
    Task<T> GetProductByIDAsync<T>(int id);
    Task<T> CreateProductAsync<T>(ProductDTO productDTO);
    Task<T> UpdateProductAsync<T>(ProductDTO productDTO);
    Task<T> DeleteProductAsync<T>(int id);
}
