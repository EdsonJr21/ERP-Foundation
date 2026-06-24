using ERPFoundation.Domain.Models;

namespace ERPFoundation.Infrastructure.Repositories.Interfaces;

public interface ISupplierRepository
{
    Task<bool> AddSupplierAsync(Supplier supplier);
    Task<List<Supplier>> ListSuppliersAsync();
    Task<Supplier?> GetByIdAsync(int id);
    Task<Supplier?> GetByTaxIdAsync(string taxId);
    Task<bool> UpdateSupplierAsync(Supplier supplier);
    Task<bool> RemoveSupplierAsync(Supplier supplier);
}
