using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Exceptions;
using ERPFoundation.Domain.Models;
using ERPFoundation.Infrastructure.Repositories.Interfaces;

namespace ERPFoundation.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository);
        _productRepository = productRepository;
    }

    public async Task CreateProductAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        NormalizeProduct(product);

        if (await _productRepository.ExistsSkuAsync(product.Sku))
        {
            throw new DomainException($"SKU '{product.Sku}' already exists.");
        }

        if (!await _productRepository.ExistsSupplierAsync(product.SupplierId))
        {
            throw new NotFoundException($"Supplier with id {product.SupplierId} was not found.");
        }

        await _productRepository.AddProductsAsync(product);
    }

    public async Task<List<Product>> ListProductsAsync()
    {
        return await _productRepository.ListProductsAsync();
    }

    public async Task<List<Product>> SearchProductsAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new List<Product>();
        }

        return await _productRepository.SearchProductsAsync(name.Trim());
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new DomainException("Invalid product ID.");
        }

        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new NotFoundException("Product not found.");
        }

        return product;
    }

    public async Task UpdateProductsAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (product.Id <= 0)
        {
            throw new DomainException("Product id must be greater than zero.");
        }

        NormalizeProduct(product);

        var existingProduct = await _productRepository.GetByIdAsync(product.Id);

        if (existingProduct is null)
        {
            throw new NotFoundException($"Product with id {product.Id} was not found.");
        }

        if (await _productRepository.ExistsSkuAsync(product.Sku, product.Id))
        {
            throw new DomainException($"SKU '{product.Sku}' already exists.");
        }

        if (!await _productRepository.ExistsSupplierAsync(product.SupplierId))
        {
            throw new NotFoundException($"Supplier with id {product.SupplierId} was not found.");
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.SupplierId = product.SupplierId;

        await _productRepository.UpdateProductsAsync(existingProduct);
    }

    public async Task RemoveProductsAsync(int id)
    {
        if (id <= 0)
        {
            throw new DomainException("Product id must be greater than zero.");
        }

        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new NotFoundException($"Product with id {id} was not found.");
        }

        if (product.Quantity > 0)
        {
            throw new DomainException("Products with stock cannot be removed.");
        }

        await _productRepository.RemoveProductsAsync(product);
    }

    private static void NormalizeProduct(Product product)
    {
        product.Name = product.Name.Trim();
        product.Sku = product.Sku.Trim().ToUpperInvariant();
    }
}