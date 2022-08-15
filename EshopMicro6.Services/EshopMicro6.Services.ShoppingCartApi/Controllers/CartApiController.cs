using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Integration.MessageBus;
using EshopMicro6.Services.ShoppingCartApi.DTOs;
using EshopMicro6.Services.ShoppingCartApi.Interfaces;
using EshopMicro6.Services.ShoppingCartApi.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EshopMicro6.Services.ShoppingCartApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartApiController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMessageBus _messageBus;
        protected ResponseDTO _response;

        public CartApiController(ICartRepository cartRepository, IMessageBus messageBus)
        {
            _cartRepository = cartRepository;
            _messageBus = messageBus;
            this._response = new ResponseDTO();
        }

        [HttpGet("GetCart/{userID}")]
        public async Task<object> GetCart(string userID)
        {
            try 
            {
                CartDTO cartDTO = await _cartRepository.GetCartByUserID(userID);
                _response.Result = cartDTO;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> () { ex.ToString() };
            }

            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDTO cartDTO)
        {
            try 
            {
                CartDTO cart = await _cartRepository.UpsertCart(cartDTO);
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> () { ex.ToString() };
            }

            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDTO cartDTO)
        {
            try 
            {
                CartDTO cart = await _cartRepository.UpsertCart(cartDTO);
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> () { ex.ToString() };
            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody]int cartID)
        {
            try 
            {
                _response.IsSuccess = await _cartRepository.RemoveFromCart(cartID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> () { ex.ToString() };
            }

            return _response;
        }
        
        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody]CartDTO cartDTO)
        {
            try 
            {
                _response.IsSuccess = await _cartRepository.ApplyCoupon(cartDTO.cartHeader.UserID, cartDTO.cartHeader.CouponCode);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> () { ex.ToString() };
            }

            return _response;
        }
        
        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody]string userID)
        {
            try 
            {
                _response.IsSuccess = await _cartRepository.RemoveCoupon(userID);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> () { ex.ToString() };
            }

            return _response;
        }

        [Authorize]
        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDTO checkoutHeaderDTO)
        {
            try 
            {
                CartDTO cartDTO = await _cartRepository.GetCartByUserID(checkoutHeaderDTO.UserID);
                if (cartDTO == null)
                {
                    return BadRequest();
                }

                checkoutHeaderDTO.cartDetails = cartDTO.CartDetails;

                //logic to add message 
                await _messageBus.PublishMessage(checkoutHeaderDTO, "checkoutmessagetopic");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> () { ex.ToString() };
            }

            return _response;
        }
    }
}