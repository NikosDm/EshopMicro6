using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;
using EshopMicro6.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EshopMicro6.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Products()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            List<ProductDTO> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDTO>(accessToken);

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDTO>(accessToken, product);

                if (response != null && response.IsSuccess) 
                {
                    return RedirectToAction(nameof(Products));
                }
            }

            return View(product);
        }

        public async Task<IActionResult> EditProduct(int productID)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIDAsync<ResponseDTO>(accessToken, productID);

            if (response != null && response.IsSuccess) 
            {
                ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductDTO product)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDTO>(accessToken, product);

                if (response != null && response.IsSuccess) 
                {
                    return RedirectToAction(nameof(Products));
                }
            }

            return View(product);
        }
        
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int productID)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.GetProductByIDAsync<ResponseDTO>(accessToken, productID);

            if (response != null && response.IsSuccess) 
            {
                ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(ProductDTO product)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDTO>(accessToken, product.ProductId);

                if (response != null && response.IsSuccess) 
                {
                    return RedirectToAction(nameof(Products));
                }
            }

            return View(product);
        }
    }
}