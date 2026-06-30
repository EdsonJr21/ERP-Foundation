using ERPFoundation.Domain.Models;
using ERPFoundation.Infrastructure.Data;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.Infrastructure.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        await _context.Suppliers.AddAsync(supplier);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<Supplier>> ListSuppliersAsync()
    {
        return await _context.Suppliers.ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Suppliers
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Supplier?> GetByTaxIdAsync(string taxId)
    {
        return await _context.Suppliers
            .FirstOrDefaultAsync(f => f.TaxId == taxId);
    }

    public async Task<bool> UpdateSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        _context.Suppliers.Update(supplier);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        _context.Suppliers.Remove(supplier);

        return await _context.SaveChangesAsync() > 0;
    }
}