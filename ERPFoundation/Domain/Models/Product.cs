using System.ComponentModel.DataAnnotations;

namespace ERPFoundation.Domain.Models;

public class Product
{
    public int Id { get; set; }
    [Required] [StringLength(100)] public string Name { get; set; } = string.Empty;
    [Required] [StringLength(50)] public string Sku { get; set; } = string.Empty;
    [Range(0.01, 999999)] public decimal Price { get; set; }
    [Range(0, int.MaxValue)] public int Quantity { get; set; }

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public Product()
    {
    }

    public Product(int id, string sku, string name, decimal price, int quantity)
    {
        Id = id;
        Sku = sku;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public Product(string sku, string name, decimal price, int quantity, int supplierId)
    {
        Sku = sku;
        Name = name;
        Price = price;
        Quantity = quantity;
        SupplierId = supplierId;
    }
}
