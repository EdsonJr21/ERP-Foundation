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

        try
        {
            await _context.Suppliers.AddAsync(supplier);

            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Error adding supplier to the database.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error adding supplier.", ex);
        }
    }

    public async Task<List<Supplier>> ListSuppliersAsync()
    {
        try
        {
            return await _context.Suppliers.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error listing suppliers.", ex);
        }
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error searching supplier by ID.", ex);
        }
    }

    public async Task<Supplier?> GetByTaxIdAsync(string taxId)
    {
        try
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(f => f.TaxId == taxId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error searching supplier by Tax ID.", ex);
        }
    }

    public async Task<bool> UpdateSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        try
        {
            _context.Suppliers.Update(supplier);

            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Error updating supplier in the database.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error updating supplier.", ex);
        }
    }

    public async Task<bool> RemoveSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        try
        {
            _context.Suppliers.Remove(supplier);

            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Error removing supplier from the database.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error removing supplier.", ex);
        }
    }
}
