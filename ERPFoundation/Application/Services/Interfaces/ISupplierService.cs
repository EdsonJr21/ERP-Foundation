using ERPFoundation.Domain.Models;

namespace ERPFoundation.Application.Services.Interfaces;

public interface ISupplierService
{
    Task<bool> AddSupplierAsync(Supplier supplier);
    Task<List<Supplier>> ListSuppliersAsync();
    Task<Supplier?> GetByIdAsync(int id);
    Task<bool> UpdateSupplierAsync(Supplier supplier);
    Task<bool> RemoveSupplierAsync(int id);
}
