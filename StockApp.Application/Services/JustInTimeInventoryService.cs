using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class JustInTimeInventoryService : IJustInTimeInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public JustInTimeInventoryService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task OptimizeInventoryAsync()
        {
            // Implementação da otimização de inventário just-in-time
            var recentOrders = await _orderRepository.GetRecentOrdersAsync();

            foreach (var order in recentOrders)
            {
                foreach (var product in order.Products)
                {
                    var currentStock = await _productRepository.GetStockAsync(product.Id);
                    var newStock = currentStock - 1; // Exemplo simples de redução do estoque
                    await _productRepository.UpdateStockAsync(product.Id, newStock);
                }
            }
        }
    }
}
