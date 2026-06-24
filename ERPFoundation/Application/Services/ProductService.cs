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

        if (string.IsNullOrWhiteSpace(product.Name) ||
            string.IsNullOrWhiteSpace(product.Sku) ||
            product.Price <= 0 ||
            product.Quantity < 0 ||
            product.SupplierId <= 0)
        {
            return false;
        }

        product.Sku = product.Sku.Trim().ToUpperInvariant();
        product.Name = product.Name.Trim();

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

        return await _productRepository.SearchProductsAsync(name);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        if (id <= 0) return null;

        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateProductsAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (product.Id <= 0 ||
            string.IsNullOrWhiteSpace(product.Name) ||
            string.IsNullOrWhiteSpace(product.Sku) ||
            product.Price <= 0 ||
            product.Quantity < 0 ||
            product.SupplierId <= 0)
        {
            return false;
        }

        product.Name = product.Name.Trim();
        product.Sku = product.Sku.Trim().ToUpperInvariant();

        if (!await _productRepository.ExistsSupplierAsync(product.SupplierId))
        {
            return false;
        }

        return await _productRepository.UpdateProductsAsync(product);
    }

    public async Task<bool> RemoveProductsAsync(int id)
    {
        if (id <= 0) return false;

        var product = await _productRepository.GetByIdAsync(id);

        if (product == null) return false;
        if (product.Quantity > 0) return false;

        return await _productRepository.RemoveProductsAsync(product);
    }
}