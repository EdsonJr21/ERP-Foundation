using ERPFoundation.Domain.Models;

namespace ERPFoundation.Tests.Builders;

public class ProductBuilder
{
    private readonly Product _product = new();

    public ProductBuilder WithValidData()
    {
        _product.Id = 1;
        _product.Name = "Notebook";
        _product.Sku = "NB-001";
        _product.Price = 3500;
        _product.Quantity = 10;
        _product.SupplierId = 1;

        return this;
    }

    public ProductBuilder WithId(int id)
    {
        _product.Id = id;
        return this;
    }

    public ProductBuilder WithQuantity(int quantity)
    {
        _product.Quantity = quantity;
        return this;
    }

    public Product Build() => _product;
}