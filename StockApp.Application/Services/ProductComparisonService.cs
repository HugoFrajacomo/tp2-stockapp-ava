using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class ProductComparisonService : IProductComparisonService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductComparisonService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> CompareProductsAsync(List<int> productIds)
        {
            var products = await _productRepository.GetByIdsAsync(productIds);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
    }
}
