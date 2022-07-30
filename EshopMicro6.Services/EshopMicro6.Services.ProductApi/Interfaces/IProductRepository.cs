using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.ProductApi.DTOs;

namespace EshopMicro6.Services.ProductApi.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProduct(int productId);
        Task<ProductDTO> UpsertProduct(ProductDTO productDTO);
        Task<bool> DeleteProduct(int productId);
    }
}