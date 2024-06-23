using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductById(int? id)
        {
            var product = await _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task Add(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.Create(product);
        }

        public async Task Update(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.Update(product);
        }

        public async Task Remove(int? id)
        {
            var product = await _productRepository.GetById(id);
            await _productRepository.Remove(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetFilteredAsync(string name, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productRepository.GetFilteredAsync(name, minPrice, maxPrice);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task BulkUpdateAsync(IEnumerable<ProductDTO> products)
        {
            foreach (var productDto in products)
            {
                var product = _mapper.Map<Product>(productDto);
                await _productRepository.Update(product);
            }
        }

        public async Task<IEnumerable<ProductDTO>> BuscaProdutosComEstoqueBaixo(int limiteEstoque)
        {
            var products = await _productRepository.GetProducts();
            var lowStockProducts = products.Where(p => p.Stock < limiteEstoque);
            return _mapper.Map<IEnumerable<ProductDTO>>(lowStockProducts);
        }
    }
}
