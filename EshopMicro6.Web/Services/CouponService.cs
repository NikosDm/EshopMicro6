using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EshopMicro6.Web.Models;
using EshopMicro6.Web.Services.IServices;

namespace EshopMicro6.Web.Services
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest 
            { 
                ApiType = SD.ApiType.Get,
                Url = SD.CouponAPIBase + "/api/coupon/" + couponCode,
                Token = token
            });
        }
    }
}