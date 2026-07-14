using AutoMapper;
using ERPFoundation.API.DTOs.Products;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERPFoundation.API.Controllers.Products;

[ApiController]
[Route("api/products")]
public class ProductsController(IProductService productService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var products = await productService.ListProductsAsync();

        var productsDto = mapper.Map<List<ProductResponseDto>>(products);

        return Ok(productsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await productService.GetByIdAsync(id);

        if (product is null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        var productDto = mapper.Map<ProductResponseDto>(product);

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var product = mapper.Map<Product>(dto);

        var result = await productService.CreateProductAsync(product);

        if (!result)
        {
            throw new ArgumentException("Invalid data, SKU already registered, or invalid supplier.");
        }

        var productDto = mapper.Map<ProductResponseDto>(product);

        return CreatedAtAction(
            nameof(GetById),
            new { id = productDto.Id },
            productDto
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        var product = await productService.GetByIdAsync(id);

        if (product is null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        mapper.Map(dto, product);

        var result = await productService.UpdateProductsAsync(product);

        return !result ? throw new ArgumentException("Could not update the product.") : NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        var existingProduct = await productService.GetByIdAsync(id);

        if (existingProduct is null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        var result = await productService.RemoveProductsAsync(id);

        return !result ? throw new ArgumentException("Could not remove the product.") : NoContent();
    }
}