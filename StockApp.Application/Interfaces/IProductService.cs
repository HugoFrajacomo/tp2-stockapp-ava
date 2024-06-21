using StockApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int? id);
        Task Add(ProductDTO productDto);
        Task Update(ProductDTO productDto);
        Task Remove(int? id);
        Task<IEnumerable<ProductDTO>> BuscaProdutosComEstoqueBaixo(int limiteEstoque);
        Task BulkUpdateAsync(List<ProductDTO> products);
    }
}
