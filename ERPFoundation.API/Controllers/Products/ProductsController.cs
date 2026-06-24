using ERPFoundation.API.DTOs.Products;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERPFoundation.API.Controllers.Products;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var products = await _productService.ListProductsAsync();

        var productsDto = products.Select(p => new ProductDto(
            p.Id,
            p.Name,
            p.Sku,
            p.Price,
            p.Quantity,
            p.SupplierId
        ));

        return Ok(productsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound("Product not found.");
        }

        var productDto = new ProductDto(
            product.Id,
            product.Name,
            product.Sku,
            product.Price,
            product.Quantity,
            product.SupplierId
        );

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Sku = dto.Sku,
            Price = dto.Price,
            Quantity = dto.Quantity,
            SupplierId = dto.SupplierId
        };

        var result = await _productService.CreateProductAsync(product);

        if (!result)
        {
            return BadRequest("Invalid data, SKU already registered, or invalid supplier.");
        }

        var productDto = new ProductDto(
            product.Id,
            product.Name,
            product.Sku,
            product.Price,
            product.Quantity,
            product.SupplierId
        );

        return CreatedAtAction(
            nameof(GetById),
            new { id = productDto.Id },
            productDto
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound("Product not found.");
        }

        product.Name = dto.Name;
        product.Sku = dto.Sku;
        product.Price = dto.Price;
        product.Quantity = dto.Quantity;
        product.SupplierId = dto.SupplierId;

        var result = await _productService.UpdateProductsAsync(product);

        if (!result)
        {
            return BadRequest("Could not update the product.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        var existingProduct = await _productService.GetByIdAsync(id);

        if (existingProduct is null)
        {
            return NotFound("Product not found.");
        }

        var result = await _productService.RemoveProductsAsync(id);

        if (!result)
        {
            return BadRequest("Could not remove the product.");
        }

        return NoContent();
    }
}