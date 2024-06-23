using StockApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetById(int? id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Remove(Product product);
        Task<IEnumerable<Product>> GetFilteredAsync(string name, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids);
        Task<int> GetStockAsync(int productId); // Adiciona método para obter o estoque de um produto
        Task UpdateStockAsync(int productId, int newStock); // Adiciona método para atualizar o estoque de um produto
    }
}
