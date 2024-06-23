using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductComparisonService _productComparisonService;

        public ProductsController(IProductService productService, IProductComparisonService productComparisonService)
        {
            _productService = productService;
            _productComparisonService = productComparisonService;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not Found");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            await _productService.Add(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest("Inconsistent Id");
            }
            if (product == null)
            {
                return BadRequest("Update Data Invalid");
            }
            await _productService.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            await _productService.Remove(id);
            return Ok("Product deleted");
        }

        [HttpGet("lowstock", Name = "GetLowStockProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetLowStockProducts(int limiteEstoque)
        {
            var products = await _productService.BuscaProdutosComEstoqueBaixo(limiteEstoque);
            if (products == null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpPut("bulk-update", Name = "BulkUpdateProducts")]
        public async Task<IActionResult> BulkUpdate([FromBody] List<ProductDTO> products)
        {
            if (products == null || !products.Any())
            {
                return BadRequest("Products is null or empty");
            }

            await _productService.BulkUpdateAsync(products);
            return Ok("Products updated");
        }

        [HttpGet("filter", Name = "FilterProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetFilteredAsync(string name, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productService.GetFilteredAsync(name, minPrice, maxPrice);
            if (products == null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpPost("compare")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> CompareProducts([FromBody] List<int> productIds)
        {
            var products = await _productComparisonService.CompareProductsAsync(productIds);
            return Ok(products);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportToCsv()
        {
            var products = await _productService.GetProducts();
            if (products == null || !products.Any())
            {
                return NotFound("No products available to export.");
            }

            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Description,Price,Stock");

            foreach (var product in products)
            {
                csv.AppendLine($"{product.Id},{product.Name},{product.Description},{product.Price},{product.Stock}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "products.csv");
        }
    }
}
