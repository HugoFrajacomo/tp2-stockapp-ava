using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using StockApp.Domain.Interfaces;
using StockApp.Domain.Entities;

namespace StockApp.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IDistributedCache _cache;
        private readonly IOrderRepository _orderRepository;

        public RecommendationService(IDistributedCache cache, IOrderRepository orderRepository)
        {
            _cache = cache;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Product>> GetRecommendationsAsync(string userId)
        {
            string cacheKey = $"recommendations_{userId}";
            string cachedRecommendations = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedRecommendations))
            {
                return JsonSerializer.Deserialize<IEnumerable<Product>>(cachedRecommendations);
            }

            var userOrders = await _orderRepository.GetByUserIdAsync(userId);
            var recommendedProducts = userOrders.SelectMany(order => order.Products)
                                                .GroupBy(product => product.Id)
                                                .OrderByDescending(group => group.Count())
                                                .Select(group => group.First())
                                                .Take(5);

            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(recommendedProducts), cacheOptions);

            return recommendedProducts;
        }
    }
}
