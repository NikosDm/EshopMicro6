using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.CouponApi.DTOs;
using EshopMicro6.Services.CouponApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EshopMicro6.Services.CouponApi.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponApiController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        protected ResponseDTO _response;

        public CouponApiController(ICouponRepository couponRepository)
        {
            this._response = new ResponseDTO();
            _couponRepository = couponRepository;
        }

        [HttpGet("{code}")]
        public async Task<object> GetDiscountForCode(string code)
        {
            try 
            {
                var coupon = await _couponRepository.GetCouponByCode(code);
                _response.Result = coupon;
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