using StockApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IProductComparisonService
    {
        Task<IEnumerable<ProductDTO>> CompareProductsAsync(List<int> productIds);
    }
}
