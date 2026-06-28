using AutoMapper;
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
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var products = await _productService.ListProductsAsync();

        var productsDto = _mapper.Map<List<ProductResponseDto>>(products);

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

        var productDto = _mapper.Map<ProductResponseDto>(product);

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        var result = await _productService.CreateProductAsync(product);

        if (!result)
        {
            return BadRequest("Invalid data, SKU already registered, or invalid supplier.");
        }

        var productDto = _mapper.Map<ProductResponseDto>(product);

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

        _mapper.Map(dto, product);

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