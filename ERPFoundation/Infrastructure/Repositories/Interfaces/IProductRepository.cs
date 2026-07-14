using ERPFoundation.Domain.Models;

namespace ERPFoundation.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    Task<bool> AddProductsAsync(Product product);
    Task<List<Product>> ListProductsAsync();
    Task<List<Product>> SearchProductsAsync(string name);

    Task<bool> UpdateProductsAsync(Product product);

    Task<Product?> GetByIdAsync(int id);

    Task<bool> RemoveProductsAsync(Product product);
    Task<bool> ExistsSkuAsync(string sku, int? productId = null);
    Task<bool> ExistsSupplierAsync(int supplierId);
}