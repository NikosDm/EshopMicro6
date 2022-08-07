using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;
using EshopMicro6.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EshopMicro6.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(ILogger<CartController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> CartIndex()
        {
            var cart = await LoadCartForrLoggedInUser();
            return View(cart);
        }

        public async Task<IActionResult> Remove(int cartDetailsID)
        {
            var UserID = User.Claims.Where(u => u.Type =="sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCartAsync<ResponseDTO>(cartDetailsID, accessToken);

            if (response != null && response.IsSuccess) 
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        private async Task<CartDTO> LoadCartForrLoggedInUser() 
        {
            var UserID = User.Claims.Where(u => u.Type =="sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserIDAsync<ResponseDTO>(UserID, accessToken);

            CartDTO cartDTO = new();
            if (response != null && response.IsSuccess) 
            {
                cartDTO = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
            }

            if (cartDTO.cartHeader != null) 
            {
                foreach (var detail in cartDTO.CartDetails)
                {
                    cartDTO.cartHeader.OrderTotal += detail.Product.Price * detail.Count;
                }
            }

            return cartDTO;
        }
    }
}