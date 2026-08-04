using ERPFoundation.Domain.Exceptions;
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

    public async Task AddSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        NormalizeSupplier(supplier);

        if (!await IsTaxIdAvailableAsync(supplier.TaxId))
        {
            throw new DomainException(
                "A supplier with this TaxId already exists."
            );
        }

        await _supplierRepository.AddSupplierAsync(supplier);
    }

    public async Task<List<Supplier>> ListSuppliersAsync()
    {
        return await _supplierRepository.ListSuppliersAsync();
    }

    public async Task<Supplier> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new DomainException("Invalid supplier ID.");
        }

        var supplier = await _supplierRepository.GetByIdAsync(id);

        if (supplier is null)
        {
            throw new NotFoundException("Supplier not found.");
        }

        return supplier;
    }

    public async Task UpdateSupplierAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        if (supplier.Id <= 0)
        {
            throw new DomainException("Invalid supplier ID.");
        }

        NormalizeSupplier(supplier);

        if (!await IsTaxIdAvailableAsync(supplier.TaxId, supplier.Id))
        {
            throw new DomainException(
                "A supplier with this TaxId already exists."
            );
        }

        var existingSupplier = await _supplierRepository.GetByIdAsync(supplier.Id);

        if (existingSupplier is null)
        {
            throw new NotFoundException($"Supplier with ID {supplier.Id} was not found.");
        }

        if (existingSupplier.TaxId != supplier.TaxId)
        {
            throw new DomainException("Supplier TaxId cannot be changed.");
        }

        existingSupplier.Name = supplier.Name;
        existingSupplier.Address = supplier.Address;

        await _supplierRepository.UpdateSupplierAsync(existingSupplier);
    }

    public async Task RemoveSupplierAsync(int id)
    {
        if (id <= 0)
        {
            throw new DomainException("Invalid supplier ID.");
        }

        var supplier = await _supplierRepository.GetByIdAsync(id);

        if (supplier is null)
        {
            throw new NotFoundException($"Supplier with ID {id} was not found.");
        }

        if (supplier.Products.Any())
        {
            throw new DomainException(
                "Cannot remove supplier with linked products."
            );
        }

        await _supplierRepository.RemoveSupplierAsync(supplier);
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
