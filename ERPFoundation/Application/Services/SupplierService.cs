using ERPFoundation.Domain.Models;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Infrastructure.Repositories.Interfaces;

namespace ERPFoundation.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        ArgumentNullException.ThrowIfNull(supplierRepository);
        _supplierRepository = supplierRepository;
    }

    public async Task<bool> AddSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        if (string.IsNullOrWhiteSpace(supplier.Name) ||
            string.IsNullOrWhiteSpace(supplier.TaxId) ||
            string.IsNullOrWhiteSpace(supplier.Address))
            return false;

        if (supplier.Name.Trim().Length < 3 || supplier.TaxId.Trim().Length != 14)
            return false;

        var existingSupplier =
            await _supplierRepository.GetByTaxIdAsync(supplier.TaxId);

        if (existingSupplier != null)
            return false;

        return await _supplierRepository.AddSupplierAsync(supplier);
    }

    public async Task<List<Supplier>> ListSuppliersAsync()
    {
        return await _supplierRepository.ListSuppliersAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _supplierRepository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        if (string.IsNullOrWhiteSpace(supplier.Name) ||
            string.IsNullOrWhiteSpace(supplier.TaxId) ||
            string.IsNullOrWhiteSpace(supplier.Address))
            return false;

        var existingSupplier = await _supplierRepository.GetByIdAsync(supplier.Id);

        if (existingSupplier == null) return false;

        existingSupplier.Name = supplier.Name;
        existingSupplier.TaxId = supplier.TaxId;
        existingSupplier.Address = supplier.Address;

        return await _supplierRepository.UpdateSupplierAsync(existingSupplier);
    }

    public async Task<bool> RemoveSupplierAsync(int id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);

        if (supplier == null) return false;

        return await _supplierRepository.RemoveSupplierAsync(supplier);
    }
}
