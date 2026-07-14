using ERPFoundation.Domain.Models;

namespace ERPFoundation.Tests.Builders;

public class SupplierBuilder
{
    private readonly Supplier _supplier = new()
    {
        Id = 1,
        Name = "Microsoft",
        TaxId = "12345678901234",
        Address = "Redmond"
    };

    public SupplierBuilder WithValidData()
    {
        return this;
    }

    public SupplierBuilder WithTaxId(string taxId)
    {
        _supplier.TaxId = taxId;
        return this;
    }

    public SupplierBuilder WithName(string name)
    {
        _supplier.Name = name;
        return this;
    }

    public SupplierBuilder WithAddress(string address)
    {
        _supplier.Address = address;
        return this;
    }

    public SupplierBuilder WithId(int id)
    {
        _supplier.Id = id;
        return this;
    }

    public Supplier Build() => _supplier;
}
