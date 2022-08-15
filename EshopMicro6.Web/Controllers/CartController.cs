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
        private readonly ICouponService _couponService;

        public CartController(ILogger<CartController> logger, IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> CartIndex()
        {
            var cart = await LoadCartForrLoggedInUser();
            return View(cart);
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDTO cartDTO)
        {
            var UserID = User.Claims.Where(u => u.Type =="sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.ApplyCouponAsync<ResponseDTO>(cartDTO, accessToken);

            if (response != null && response.IsSuccess) 
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDTO cartDTO)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveCouponAsync<ResponseDTO>(cartDTO.cartHeader.UserID, accessToken);

            if (response != null && response.IsSuccess) 
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        public async Task<IActionResult> Checkout()
        {
            var cart = await LoadCartForrLoggedInUser();
            return View(cart);
        }
        
        [HttpPost]
        public async Task<IActionResult> Checkout(CartDTO cartDTO)
        {
            try 
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _cartService.CheckoutAsync<ResponseDTO>(cartDTO.cartHeader, accessToken);

                if (!response.IsSuccess)
                {
                    TempData["Error"] = response.Message;
                    return RedirectToAction(nameof(Checkout));
                }

                return RedirectToAction(nameof(Confirmation));
            }
            catch 
            {
                return View(cartDTO);
            }
        }

        public async Task<IActionResult> Confirmation()
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
                if (!string.IsNullOrWhiteSpace(cartDTO.cartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCoupon<ResponseDTO>(cartDTO.cartHeader.CouponCode, accessToken);
                    if (coupon != null && coupon.IsSuccess)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(coupon.Result));
                        cartDTO.cartHeader.DiscountTotal = couponObj.DiscountAmount;
                    }
                }

                foreach (var detail in cartDTO.CartDetails)
                {
                    cartDTO.cartHeader.OrderTotal += detail.Product.Price * detail.Count;
                }

                cartDTO.cartHeader.OrderTotal -= cartDTO.cartHeader.DiscountTotal;
            }

            return cartDTO;
        }
    }
}