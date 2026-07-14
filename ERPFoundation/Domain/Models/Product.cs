namespace ERPFoundation.Domain.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public Product()
    {
    }

    public Product(
        string sku,
        string name,
        decimal price,
        int quantity,
        int supplierId)
    {
        Sku = sku;
        Name = name;
        Price = price;
        Quantity = quantity;
        SupplierId = supplierId;
    }
}