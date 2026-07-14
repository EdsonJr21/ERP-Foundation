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

        NormalizeSupplier(supplier);

        if (!await IsTaxIdAvailableAsync(supplier.TaxId))
        {
            return false;
        }

        return await _supplierRepository.AddSupplierAsync(supplier);
    }

    public async Task<List<Supplier>> ListSuppliersAsync()
    {
        return await _supplierRepository.ListSuppliersAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        return await _supplierRepository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        if (supplier.Id <= 0)
        {
            return false;
        }

        NormalizeSupplier(supplier);

        if (!await IsTaxIdAvailableAsync(supplier.TaxId, supplier.Id))
        {
            return false;
        }

        var existingSupplier = await _supplierRepository.GetByIdAsync(supplier.Id);

        if (existingSupplier is null)
        {
            return false;
        }

        existingSupplier.Name = supplier.Name;
        existingSupplier.TaxId = supplier.TaxId;
        existingSupplier.Address = supplier.Address;

        return await _supplierRepository.UpdateSupplierAsync(existingSupplier);
    }

    public async Task<bool> RemoveSupplierAsync(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        var supplier = await _supplierRepository.GetByIdAsync(id);

        if (supplier is null)
        {
            return false;
        }

        return await _supplierRepository.RemoveSupplierAsync(supplier);
    }

    private async Task<bool> IsTaxIdAvailableAsync(string taxId, int? supplierId = null)
    {
        var existingSupplier = await _supplierRepository.GetByTaxIdAsync(taxId);
        
        if (existingSupplier is null)
        {
            return true; 
        }
        
        if (supplierId.HasValue && existingSupplier.Id == supplierId.Value)
        {
            return true;
        }
        
        return false;
    }

    private static void NormalizeSupplier(Supplier supplier)
    {
        supplier.Name = supplier.Name.Trim();
        supplier.TaxId = supplier.TaxId.Trim();
        supplier.Address = supplier.Address.Trim();
    }
}
