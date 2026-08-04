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
        
        var productDto = mapper.Map<ProductResponseDto>(product);

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var product = mapper.Map<Product>(dto);

        await productService.CreateProductAsync(product);

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
        
        mapper.Map(dto, product);

        await productService.UpdateProductsAsync(product);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        await productService.RemoveProductsAsync(id);

        return NoContent();
    }
}