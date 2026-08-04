using ERPFoundation.Domain.Models;

namespace ERPFoundation.Application.Services.Interfaces;

public interface IProductService
{
    Task CreateProductAsync(Product product);

    public Task<List<Product>> ListProductsAsync();
    public Task<List<Product>> SearchProductsAsync(string name);
    public Task<Product> GetByIdAsync(int id);
    public Task UpdateProductsAsync(Product product);
    public Task RemoveProductsAsync(int id);
}