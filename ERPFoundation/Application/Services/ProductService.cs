using ERPFoundation.Application.Services.Interfaces;
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

    public async Task<bool> CreateProductAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        NormalizeProduct(product);

        if (await _productRepository.ExistsSkuAsync(product.Sku))
        {
            return false;
        }

        if (!await _productRepository.ExistsSupplierAsync(product.SupplierId))
        {
            return false;
        }

        return await _productRepository.AddProductsAsync(product);
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

    public async Task<Product?> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateProductsAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (product.Id <= 0)
        {
            return false;
        }
        
        NormalizeProduct(product);
        
        var existingProduct = await _productRepository.GetByIdAsync(product.Id);

        if (existingProduct is null)
        {
            return false;
        }

        if (await _productRepository.ExistsSkuAsync(product.Sku, product.Id))
        {
            return false;
        }

        if (!await _productRepository.ExistsSupplierAsync(product.SupplierId))
        {
            return false;
        }

        existingProduct.Name = product.Name;
        existingProduct.Sku = product.Sku;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.SupplierId = product.SupplierId;

        return await _productRepository.UpdateProductsAsync(existingProduct);
    }

    public async Task<bool> RemoveProductsAsync(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return false;
        }

        if (product.Quantity > 0)
        {
            return false;
        }

        return await _productRepository.RemoveProductsAsync(product);
    }

    private static void NormalizeProduct(Product product)
    {
        product.Name = product.Name.Trim();
        product.Sku = product.Sku.Trim().ToUpperInvariant();
    }
}