using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EshopMicro6.Web.Models;
using Microsoft.AspNetCore.Authorization;
using EshopMicro6.Web.Services.IServices;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;

namespace EshopMicro6.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTO> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDTO>(string.Empty);

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int productID)
        {
            ProductDTO product = new();
            var response = await _productService.GetProductByIDAsync<ResponseDTO>(string.Empty, productID);

            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }

            return View(product);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsPost(ProductDTO productDTO)
        {
            CartDTO cartDTO = new()
            {
                cartHeader = new CartHeaderDTO
                {
                    UserID = User.Claims.Where(u => u.Type =="sub")?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDTO cartDetailsDTO = new CartDetailsDTO()
            {
                Count = productDTO.Count,
                ProductId = productDTO.ProductId
            };

            var response = await _productService.GetProductByIDAsync<ResponseDTO>(string.Empty, productDTO.ProductId);
            if (response != null && response.IsSuccess)
            {
                cartDetailsDTO.Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }

            List<CartDetailsDTO> cartDetailsDTOs = new();
            cartDetailsDTOs.Add(cartDetailsDTO);
            cartDTO.CartDetails = cartDetailsDTOs;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var addToCartResponse = await _cartService.AddToCartAsync<ResponseDTO>(cartDTO, accessToken);
            if (addToCartResponse != null && addToCartResponse.IsSuccess)
            {
                cartDetailsDTO.Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(addToCartResponse.Result));
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
