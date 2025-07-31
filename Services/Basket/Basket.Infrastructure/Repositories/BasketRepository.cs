using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public Task<BasketCheckout> CheckoutBasketAsync(BasketCheckout basketCheckout)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _distributedCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart?> GetBasketAsync(string userName)
        {
            var basket = await _distributedCache.GetStringAsync(userName);

            if (string.IsNullOrWhiteSpace(basket))
            {
                return null;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart?> UpsertBasketAsync(ShoppingCart shoppingCart)
        {
            await _distributedCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));

            return await GetBasketAsync(shoppingCart.UserName);
        }
    }
}
