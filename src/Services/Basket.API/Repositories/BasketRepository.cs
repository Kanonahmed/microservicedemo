using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisChache;
        public BasketRepository(IDistributedCache redisChache)
        {
            _redisChache=redisChache;
        }
        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket= await _redisChache.GetStringAsync(userName);
            if(string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisChache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisChache.SetStringAsync(basket.UserName,JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
