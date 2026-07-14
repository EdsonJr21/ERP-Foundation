using ERPFoundation.Domain.Models;
using ERPFoundation.Infrastructure.Data;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<bool> AddProductsAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        context.Products.Add(product);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Product>> ListProductsAsync()
    {
        return await ApplyDefaultOrdering(context.Products).ToListAsync();
    }

    public async Task<List<Product>> SearchProductsAsync(string name)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            var term = name.Trim().ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(term) || p.Sku.ToLower().Contains(term));
        }

        return await ApplyDefaultOrdering(query).ToListAsync();
    }

    public async Task<bool> UpdateProductsAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        context.Products.Update(product);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<bool> RemoveProductsAsync(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return true;
    }

    private static IQueryable<Product> ApplyDefaultOrdering(IQueryable<Product> query)
    {
        return query
            .OrderByDescending(p => p.Quantity > 0)
            .ThenBy(p => p.Name)
            .ThenBy(p => p.Price);
    }

    public async Task<bool> ExistsSkuAsync(string sku, int? productId = null)
    {
        if (string.IsNullOrWhiteSpace(sku)) return false;

        var skuNormalized = sku.Trim().ToUpperInvariant();

        return await context.Products.AnyAsync(p =>
            p.Sku == skuNormalized && (!productId.HasValue || p.Id != productId.Value));
    }

    public async Task<bool> ExistsSupplierAsync(int supplierId)
    {
        if (supplierId <= 0) return false;

        return await context.Suppliers.AnyAsync(f => f.Id == supplierId);
    }
}