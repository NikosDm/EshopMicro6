using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EshopMicro6.Services.ProductApi.Data;
using EshopMicro6.Services.ProductApi.DTOs;
using EshopMicro6.Services.ProductApi.Entities;
using EshopMicro6.Services.ProductApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext; 
        private IMapper _mapper;

        public ProductRepository(AppDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try 
            {
                Product product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                
                if (product == null) return false;

                _dbContext.Remove(product);
                
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<ProductDTO> GetProduct(int productId)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            IEnumerable<Product> products = await _dbContext.Products.ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> UpsertProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<ProductDTO, Product>(productDTO);

            if (product.ProductId > 0) 
            {
                _dbContext.Products.Update(product);
            }
            else 
            {
                _dbContext.Products.Add(product);
            }
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(product);
        }
    }
}