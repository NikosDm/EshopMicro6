using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.ShoppingCartApi.DTOs;
using EshopMicro6.Services.ShoppingCartApi.Interfaces;
using Newtonsoft.Json;

namespace EshopMicro6.Services.ShoppingCartApi.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client;

        public CouponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponDTO> GetCoupon(string couponName)
        {
            var response = await _client.GetAsync($"/api/coupon/{couponName}");

            var apiContent = await response.Content.ReadAsStringAsync();

            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(resp.Result));
            }

            return new CouponDTO();
        }
    }
}