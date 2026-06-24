using System.ComponentModel.DataAnnotations;

namespace ERPFoundation.Domain.Models;

public class Supplier
{
    public int Id { get; set; }
    [Required] [StringLength(100)] public string Name { get; set; } = string.Empty;
    [Required] [StringLength(18)] public string TaxId { get; set; } = string.Empty;
    [StringLength(200)] public string Address { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}