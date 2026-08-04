using ERPFoundation.Domain.Models;

namespace ERPFoundation.Infrastructure.Repositories.Interfaces;

public interface ISupplierRepository
{
    Task AddSupplierAsync(Supplier supplier);
    Task<List<Supplier>> ListSuppliersAsync();
    Task<Supplier?> GetByIdAsync(int id);
    Task<Supplier?> GetByTaxIdAsync(string taxId);
    Task UpdateSupplierAsync(Supplier supplier);
    Task RemoveSupplierAsync(Supplier supplier);
}
