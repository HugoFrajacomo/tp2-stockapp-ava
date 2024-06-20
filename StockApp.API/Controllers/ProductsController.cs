﻿using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Application.Services;

namespace StockApp.API.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
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

        [HttpPut(Name = "Update Product")]
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

        [HttpDelete("{id:int}", Name = "Delete Product")]
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

        [HttpPut("AtualizarProdutos", Name = "AtualizarProdutos")]
        public async Task<ActionResult> AtualizarProdutos([FromBody] IEnumerable<ProductDTO> products)
        {
            if (products == null)
            {
                return BadRequest("Products is null");
            }
            await _productService.AtualizarProdutos(products);
            return Ok(products);
        }
    }
}
