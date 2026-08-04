using ERPFoundation.Domain.Models;

namespace ERPFoundation.Application.Services.Interfaces;

public interface ISupplierService
{
    Task AddSupplierAsync(Supplier supplier);
    Task<List<Supplier>> ListSuppliersAsync();
    Task<Supplier> GetByIdAsync(int id);
    Task UpdateSupplierAsync(Supplier supplier);
    Task RemoveSupplierAsync(int id);
}
