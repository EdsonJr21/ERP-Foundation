using ERPFoundation.Domain.Models;
using ERPFoundation.Infrastructure.Data;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddProductsAsync(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Product>> ListProductsAsync()
    {
        return await ApplyDefaultOrdering(_context.Products).ToListAsync();
    }

    public async Task<List<Product>> SearchProductsAsync(string name)
    {
        var query = _context.Products.AsQueryable();

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

        var existingProduct = await _context.Products.FindAsync(product.Id);
        if (existingProduct == null)
        {
            return false;
        }

        existingProduct.Name = product.Name;
        existingProduct.Sku = product.Sku;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.SupplierId = product.SupplierId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<bool> RemoveProductsAsync(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    private static IQueryable<Product> ApplyDefaultOrdering(IQueryable<Product> query)
    {
        return query
            .OrderByDescending(p => p.Quantity > 0)
            .ThenBy(p => p.Name)
            .ThenBy(p => p.Price);
    }

    public async Task<bool> ExistsSkuAsync(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku)) return false;

        var skuNormalized = sku.Trim().ToUpperInvariant();
        return await _context.Products.AnyAsync(p => EF.Functions.Like(p.Sku, skuNormalized));
    }

    public async Task<bool> ExistsSupplierAsync(int supplierId)
    {
        if (supplierId <= 0) return false;

        return await _context.Suppliers.AnyAsync(f => f.Id == supplierId);
    }
}