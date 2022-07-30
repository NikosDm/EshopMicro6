using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.ProductApi.DTOs;
using EshopMicro6.Services.ProductApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EshopMicro6.Services.ProductApi.Controllers;

[Route("api/products")]
public class ProductApiController : Controller
{
    private readonly ILogger<ProductApiController> _logger;
    protected ResponseDTO _response;
    private readonly IProductRepository _productRepository; 

    public ProductApiController(ILogger<ProductApiController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        this._response = new ResponseDTO();
    }

    [HttpGet]
    public async Task<ResponseDTO> Get() 
    {
        
        try 
        {
            IEnumerable<ProductDTO> products =  await _productRepository.GetProducts();
            _response.Result = products;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ResponseDTO> Get(int productId) 
    {
        try 
        {
            ProductDTO product =  await _productRepository.GetProduct(productId);
            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpPost]
    public async Task<ResponseDTO> Post([FromBody] ProductDTO productDTO) 
    {
        try 
        {
            ProductDTO product =  await _productRepository.UpsertProduct(productDTO);
            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpPut]
    public async Task<ResponseDTO> Put([FromBody] ProductDTO productDTO) 
    {
        try 
        {
            ProductDTO product =  await _productRepository.UpsertProduct(productDTO);
            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ResponseDTO> Delete(int productId) 
    {
        try 
        {
            bool result =  await _productRepository.DeleteProduct(productId);
            _response.Result = result;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
}
